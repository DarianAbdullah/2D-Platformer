using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hero.Command;

public class BossController : MonoBehaviour
{
    private IHeroCommand Fireball;

    private float AggroRange = 15f;
    private float WaitTime = 0.5f;
    private float FireTime = 1.75f;
    private float FireCounter = 0f;
    private float PhaseMax = 5.5f;
    private float PhaseTimer = 0f;
    private float Counter = 0;
    private bool Ground;
    private int Health = 1;
    private int PrevHealth;
    private bool Dead = false;
    private enum Phase { Attacking, Running, Fireball, Seeking, Retreating, Waiting };
    private Phase currentPhase = Phase.Waiting;
    private Rigidbody2D BossRigidBody;
    [SerializeField] GameObject player;
    [SerializeField] private float JumpStrength;
    [SerializeField] private RectTransform GameWonScreen;
    //private float AggroRange = 15f;
    public AudioSource[] audioSources;
    //private AudioSource StepAudio;
    private AudioSource HurtAudio;
    private AudioSource DeathAudio;
    //private AudioSource SpawnAudio;

    //Darian's changes
    public bool IsDead;
    private Animator animator;
    private Material matRed;
    private Material matDefault;
    SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        audioSources = GetComponents<AudioSource>();
        //StepAudio = audioSources[0];
        HurtAudio = audioSources[0];
        DeathAudio = audioSources[1];
        //SpawnAudio = audioSources[3];
        //SpawnAudio.Play(0);
        this.BossRigidBody = this.gameObject.GetComponent<Rigidbody2D>();
        this.Fireball = this.gameObject.GetComponent<BossFireballAttack>();
        // Darian's changes
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        matRed = Resources.Load("RedFlash", typeof(Material)) as Material;
        matDefault = sr.material;

    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            return;
        }
        //Debug.Log(this.currentPhase);
        if (this.currentPhase == Phase.Waiting)
        {
            var position = this.gameObject.transform.position;
            var heroPos = player.transform.position.x;
            //Debug.Log(Mathf.Abs(heroPos - position.x));
            if (Mathf.Abs(heroPos - position.x) < AggroRange)
            {
                this.currentPhase = Phase.Running;
            }
        }

        if (this.currentPhase == Phase.Running)
        {
            var position = this.gameObject.transform.position;
            var heroPos = player.transform.position.x;
            this.FireCounter += Time.deltaTime;
            if (FireCounter > FireTime)
            {
                FireCounter = 0f;
                this.Fireball.Execute(this.gameObject);
            }
            position.x += 2f * Time.deltaTime;
            this.gameObject.transform.position = position;
            //Debug.Log(position.x);

            if (position.x > 130)
            {
                this.currentPhase = Phase.Fireball;
            }
        }

        if (this.currentPhase == Phase.Fireball)
        {
            var position = this.gameObject.transform.position;
            var heroPos = player.transform.position.x;
            this.FireCounter += Time.deltaTime;
            this.PhaseTimer += Time.deltaTime;
            if (FireCounter > FireTime)
            {
                FireCounter = 0f;
                this.Fireball.Execute(this.gameObject);
            }
            if (PhaseTimer > PhaseMax)
            {
                PhaseTimer = 0f;
                this.currentPhase = Phase.Attacking;
            }

        }

        if (this.currentPhase == Phase.Attacking)
        {
            if (Health < PrevHealth)
            {
                this.currentPhase = Phase.Retreating;
            }
            var position = this.gameObject.transform.position;
            var heroPos = player.transform.position.x;
            float direction = 0f;
            if (heroPos > position.x + 0.3f || heroPos > position.x - 0.3f)
            {
                direction = 1f;
                //gameObject.GetComponent<SpriteRenderer>().flipX = true;
            }

            else if (heroPos < position.x + 0.3f || heroPos < position.x - 0.3f)
            {
                direction = -1f;
                //gameObject.GetComponent<SpriteRenderer>().flipX = false;
            }
            position.x += direction * 4f * Time.deltaTime;
            this.gameObject.transform.position = position;
        }

        if (this.currentPhase == Phase.Retreating)
        {
            var position = this.gameObject.transform.position;
            var heroPos = player.transform.position.x;
            if (position.x < 130)
            {
                position = Vector2.Lerp(position, new Vector2(131, position.y), .1f);
                this.gameObject.transform.position = position;
            }
            else
            {
                this.currentPhase = Phase.Fireball;
            }
        }

        if (Health <= 0)
        {
            gameObject.layer = 10;
            BossDeath();
        }
        PrevHealth = Health;
    }

    void BossDeath()
    {
        if (!Dead)
        {
            currentPhase = Phase.Waiting;
            DeathAudio.Play(0);
            Dead = true;
            IsDead = true;
            animator.SetBool("IsDead", IsDead);
        }
        if (!DeathAudio.isPlaying)
        {
        GameWon();
        Destroy(this.gameObject);
        }
    }

    void GameWon()
    {
        GameWonScreen.gameObject.SetActive(true);
    }

    public void BossHit(string weapon)
    {
        HurtAudio.Play(0);
        //if (currentPhase != Phase.Fireball && currentPhase != Phase.Running)
        //{
            if (weapon == "sword")
            {
                //Darian's change
                if (Health > 2)
                {
                    sr.material = matRed;
                }
                Health = Health - 2;
            }
            if (Health > 0)
            {
                Invoke("ResetMat", 0.1f);
            }
        //}
    }

    public void BossKnock(GameObject enemy)
    {
        if (Health >= 1)
        {
            var enemyLocation = enemy.transform.position;
            float xKnock = 3f;

            if (enemyLocation.x > this.transform.position.x)
            {
                xKnock = -3f;
            }
            var knockVector = new Vector2(xKnock, 7f);
            BossRigidBody.velocity = knockVector;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            Ground = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "Ground")
        {
            Ground = true;
        }

        if (collision.gameObject.tag == "Untagged")
        {
            BossHit("sword");
            Destroy(collision.gameObject);
        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            Ground = false;
        }
    }

    void PlayStepAudio()
    {
        if (Ground)
        {
            //StepAudio.Play(0);
        }
    }

    // Darian's change
    void ResetMat()
    {
        sr.material = matDefault;
    }
}


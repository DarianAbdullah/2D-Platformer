using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonController : MonoBehaviour
{
    private float WaitTime = 0.5f;
    private float Counter = 0;
    private bool Ground;
    private int Health = 1;
    private bool Dead = false;
    private Rigidbody2D SkeletonRigidBody;
    [SerializeField] GameObject player;
    [SerializeField] private float JumpStrength;
    private float AggroRange = 15f;
    public AudioSource[] audioSources;
    private AudioSource StepAudio;
    private AudioSource HurtAudio;
    private AudioSource DeathAudio;
    private AudioSource SpawnAudio;

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
        StepAudio = audioSources[0];
        HurtAudio = audioSources[1];
        DeathAudio = audioSources[2];
        SpawnAudio = audioSources[3];
        SpawnAudio.Play(0);
        this.SkeletonRigidBody = this.gameObject.GetComponent<Rigidbody2D>();

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
        Counter = Counter + Time.deltaTime;
        if (Counter > WaitTime && Dead == false)
        {
            var position = this.gameObject.transform.position;
            var heroPos = player.transform.position.x;
            float direction = 0f;
            if (heroPos > position.x + 0.3f || heroPos > position.x - 0.3f)
            {
                direction = 1f;
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
            }

            else if (heroPos < position.x + 0.3f || heroPos < position.x - 0.3f)
            {
                direction = -1f;
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
            }
            else
            {
                direction = 0f;
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
            }
            if (Mathf.Abs(heroPos - position.x) < AggroRange)
            {
                position.x += direction * 2f * Time.deltaTime;
            }
            this.gameObject.transform.position = position;
        }
        if (Health <= 0)
        {
            gameObject.layer = 10;
            SkeletonDeath();
        }     
    }

    void SkeletonDeath()
    {
        if (!Dead)
        {
            DeathAudio.Play(0);
            Dead = true;
            IsDead = true;
            animator.SetBool("IsDead", IsDead);
        }
        if (!DeathAudio.isPlaying)
        {
            Destroy(this.gameObject);
        }
    }

    public void SkeletonHit(string weapon)
    {
        HurtAudio.Play(0);
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

    }

    public void SkeletonKnock(GameObject enemy)
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
            SkeletonRigidBody.velocity = knockVector;
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
        if (collision.gameObject.tag == "Ground")
        {
            Ground = true;
        }

        if (collision.gameObject.tag == "wall")
        {
            if (Ground)
            {
                    SkeletonRigidBody.velocity += Vector2.up * this.JumpStrength;
            }
        }
        if (collision.gameObject.tag == "Untagged")
        {
            SkeletonHit("sword");
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
            StepAudio.Play(0);
        }
    }

    // Darian's change
    void ResetMat()
    {
        sr.material = matDefault;
    }
}

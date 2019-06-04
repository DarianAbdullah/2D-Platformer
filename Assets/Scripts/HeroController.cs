using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Hero.Command;

public class HeroController : MonoBehaviour
{
    public bool ground;
    public bool cooling = false;
    private bool AttackCooling = false;
    public bool dead = false;
    public string weapon = "sword";
    private float DamageCoolDown = 1f;
    private float AttackCoolDown = 0.5f;
    private float Counter = 0;
    private float AttackCounter = 0;
    private int Health = 10;
    private Rigidbody2D rb;
    private HealthBar PlayerHealthBar;
    public AudioSource[] audioSources;
    private AudioSource StepAudio;
    private AudioSource HurtAudio;
    private AudioSource LandAudio;
    private AudioSource DeathAudio;
    private AudioSource SwordAudio;
    private AudioSource JumpAudio;

    // Darian's change
    public bool IsAttacking;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        PlayerHealthBar = GetComponent<HealthBar>();
        audioSources = GetComponents<AudioSource>();
        StepAudio = audioSources[0];
        HurtAudio = audioSources[1];
        LandAudio = audioSources[2];
        DeathAudio = audioSources[3];
        SwordAudio = audioSources[4];
        JumpAudio = audioSources[5];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && !AttackCooling)
        {
            // Darian's change
            IsAttacking = true;

            AttackCooling = true;
            var attack = this.gameObject.GetComponent<SwordAttack>();
            SwordAudio.Play(0);
            if (!this.gameObject.GetComponent<SpriteRenderer>().flipX)
            {
                attack.Enable();
            }
            else
            {
                attack.EnableRev();
            }
        }

        if (ground && Input.GetButtonDown("Jump"))
        {
            JumpAudio.Play(0);
        }

        hitCoolDown();
        AttackCool();

        if (Health <= 0)
        {
            playerDeath();
        }
    }

    public int GetHealth()
    {
        return Health;
    }

    void playerDeath()
    {
        if (!dead)
        {
            DeathAudio.Play(0);
            dead = true;
        }
        if (!DeathAudio.isPlaying)
        {
            Destroy(this.gameObject);
        } 
    }       

    void hitCoolDown()
    {
        if (cooling)
        {
            Counter += Time.deltaTime;
            if (Counter > DamageCoolDown)
            {
                cooling = false;
                Counter = 0;
            }
        }
    }

    void AttackCool()
    {
        if (AttackCooling)
        {
            AttackCounter += Time.deltaTime;
            if (AttackCounter > AttackCoolDown)
            {
                // Darian's change
                IsAttacking = false;

                AttackCooling = false;
                AttackCounter = 0;
            }
        }
    }

    void playerHit(GameObject enemy)
    {
        HurtAudio.Play(0);
        if (enemy.tag == "skeleton")
        {
            Health = Health - 1;
            cooling = true;
        }
    }

    void playerKnock(GameObject enemy)
    {
        var enemyLocation = enemy.transform.position;
        float xKnock = 3f;

        if (enemyLocation.x > this.transform.position.x)
        {
            xKnock = -3f;
        }
        var knockVector = new Vector2(xKnock, 7f);
        rb.velocity = knockVector;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if ((collision.gameObject.tag == "enemy" || collision.gameObject.tag == "skeleton")
            && cooling == false)
        {
            playerHit(collision.gameObject);
            playerKnock(collision.gameObject);
        }

        if (collision.gameObject.tag == "Ground")
        {
            ground = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            ground = true;
            LandAudio.Play(0);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            ground = false;
        }
    }

    void PlayStepAudio()
    {
        if (ground)
        {
            StepAudio.Play(0);
        }
    }
}
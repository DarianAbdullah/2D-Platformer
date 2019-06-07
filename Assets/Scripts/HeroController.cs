using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Hero.Command;

public class HeroController : MonoBehaviour
{
    private IHeroCommand Attack;
    private IHeroCommand Fireball;

    public bool ground;
    public bool inArena = false;
    public bool cooling = false;
    private bool AttackCooling = false;
    public bool dead = false;
    public string weapon = "sword";
    private float DamageCoolDown = 0.5f;
    private float AttackCoolDown = 0.5f;
    private float Counter = 0;
    private float AttackCounter = 0;
    private int Health = 10;
    private Rigidbody2D rb;
    private HealthBar PlayerHealthBar;
    private GameOver GameOverScreen;
    public AudioSource[] audioSources;
    private AudioSource StepAudio;
    private AudioSource HurtAudio;
    private AudioSource LandAudio;
    private AudioSource DeathAudio;
    private AudioSource SwordAudio;
    private AudioSource JumpAudio;
    private AudioSource FireAudio;

    // Darian's change
    public bool IsAttacking;
    public bool IsCasting;
    public bool IsHurt;
    private float elapsedTime = 0.0f;
    private float hurtFrames = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.AddComponent<SwordAttack>();
        this.Attack = this.gameObject.GetComponent<SwordAttack>();
        this.Fireball = this.gameObject.GetComponent<FireballAttack>();

        rb = GetComponent<Rigidbody2D>();
        PlayerHealthBar = GetComponent<HealthBar>();
        GameOverScreen = GetComponent<GameOver>();
        audioSources = GetComponents<AudioSource>();
        StepAudio = audioSources[0];
        HurtAudio = audioSources[1];
        LandAudio = audioSources[2];
        DeathAudio = audioSources[3];
        SwordAudio = audioSources[4];
        JumpAudio = audioSources[5];
        FireAudio = audioSources[6];
    }

    // Update is called once per frame
    void Update()
    {
        if(dead)
        {
            GameOverScreen.IsGameOver();
        }

        if (Input.GetButtonDown("Fire1") && !AttackCooling)
        {
            //Darian's change
            IsAttacking = true;

            AttackCooling = true;
            SwordAudio.Play(0);
            this.Attack.Execute(this.gameObject);
        }

        if(Input.GetButtonDown("Fire2") && !AttackCooling)
        {
            IsAttacking = true;
            AttackCooling = true;

            this.Fireball.Execute(this.gameObject);
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

        if (this.transform.position.x > 95f)
        {
            inArena = true;
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
                IsHurt = false;
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

    public void playerHit(GameObject enemy)
    {
        HurtAudio.Play(0);
        if (enemy.tag == "skeleton" || enemy.tag == "skull" || enemy.tag == "enemy")
        {
            Health = Health - 1;
            cooling = true;
        }
        if (enemy.tag == "hound" || enemy.tag == "boss")
        {
            Health = Health - 2;
            cooling = true;
        }
        
    }

    public void playerKnock(GameObject enemy)
    {
        var enemyLocation = enemy.transform.position;
        float xKnock = 6f;

        if (enemy.tag == "hound" || enemy.tag == "boss")
        {
            xKnock = 12f;
        }

        if (enemyLocation.x >= this.transform.position.x)
        {       
            xKnock = 0 - xKnock;

            //Darian's change
            IsHurt = true;
        }
        var knockVector = new Vector2(xKnock, 12f);
        rb.velocity = knockVector;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if ((collision.gameObject.tag == "skeleton" || collision.gameObject.tag == "skull"
            || collision.gameObject.tag == "hound" || collision.gameObject.tag == "boss")
            && cooling == false && collision.gameObject.layer != 10)
        {
            playerHit(collision.gameObject);
            playerKnock(collision.gameObject);
        }

        if (collision.gameObject.tag == "Ground")
        {
            ground = true;
        }

        if (collision.gameObject.tag == "enemy")
        {
            playerHit(collision.gameObject);
            playerKnock(collision.gameObject);
            Destroy(collision.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            ground = true;
            LandAudio.Play(0);
        }

        if(collision.gameObject.tag == "spike")
        {
            Health = 0;
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
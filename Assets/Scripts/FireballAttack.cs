using Hero.Command;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballAttack : MonoBehaviour, IHeroCommand
{
    [SerializeField]
    public GameObject FireballPrefab;
    private GameObject Player;
    private GameObject Fireball;
    private Rigidbody2D Rigidbody;
    private CircleCollider2D FireballCollider;
    private bool Active;
    private int Counter = 0;
    private const float DURATION = 5f;
    private float ElapsedTime;
    private AudioSource sound;

    private const float VelocityX = 10.0f;
    private const float VelocityY = 0.0f;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        sound = GetComponent<HeroController>().audioSources[6];
        this.Active = false;
        this.ElapsedTime = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        // Change by Heping 
        //Active = false;
        this.ElapsedTime += Time.deltaTime;
        if(this.ElapsedTime > DURATION)
        {
            Active = false;
        }
    }

    public float GetFireCooldown()
    {
        return this.ElapsedTime;
    }

    public float GetDuration()
    {
        return DURATION;
    }

    public void Execute(GameObject gameObject)
    {
        if (!Active)
        {
            this.ElapsedTime = 0;
            Active = true;
            animator.SetBool("IsFire", Active);
            Player = gameObject;
            var playerPosition = Player.transform.position;

            bool xDirection = Player.GetComponent<SpriteRenderer>().flipX;
            sound.Play(0);
            if (xDirection)
            {
                Fireball = (GameObject)Instantiate(FireballPrefab, new Vector3(playerPosition.x - 1.4f, 
                    playerPosition.y, playerPosition.z), Player.transform.rotation);
                Rigidbody = Fireball.GetComponent<Rigidbody2D>();
                Rigidbody.velocity = new Vector2(-VelocityX, VelocityY);
            }
            else 
            {
                Fireball = (GameObject)Instantiate(FireballPrefab, new Vector3(playerPosition.x + 1.4f,
                    playerPosition.y, playerPosition.z), Player.transform.rotation);
                Rigidbody = Fireball.GetComponent<Rigidbody2D>();
                Rigidbody.velocity = new Vector2(VelocityX, VelocityY);
            }

            FireballCollider = Fireball.GetComponent<CircleCollider2D>();
            FireballCollider.enabled = true;
        }
    }
}

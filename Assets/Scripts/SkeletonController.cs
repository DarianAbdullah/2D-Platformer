using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonController : MonoBehaviour
{
    private float WaitTime = 0.5f;
    private float Counter = 0;
    private bool Ground;
    private int Health = 5;
    private bool Dead = false;
    private Rigidbody2D SkeletonRigidBody;
    [SerializeField] GameObject player;
    [SerializeField] private float JumpStrength;
    [SerializeField] AudioSource StepAudio;
    [SerializeField] AudioSource HurtAudio;
    [SerializeField] AudioSource DeathAudio;
    // Start is called before the first frame update
    void Start()
    {
        this.SkeletonRigidBody = this.gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            return;
        }
        Counter = Counter + Time.deltaTime;
        if (Counter > WaitTime)
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
            position.x += direction * 2f * Time.deltaTime;
            this.gameObject.transform.position = position;
        }
        if (Health <= 0)
        {
            SkeletonDeath();
        }
        
    }

    void SkeletonDeath()
    {
        if (!Dead)
        {
            DeathAudio.Play(0);
            Dead = true;
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
            Health = Health - 2;
        }
        
    }

    public void SkeletonKnock(GameObject enemy)
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
}

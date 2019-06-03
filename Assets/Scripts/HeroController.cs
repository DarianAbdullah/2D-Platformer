using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Hero.Command;

public class HeroController : MonoBehaviour
{
    public bool ground;
    public bool cooling = false;
    private float DamageCoolDown = 1f;
    private float Counter = 0;
    private int Health = 10;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        hitCoolDown();

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
        Destroy(this.gameObject);
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

    void playerHit(GameObject enemy)
    {
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
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            ground = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            ground = false;
        }
    }
}
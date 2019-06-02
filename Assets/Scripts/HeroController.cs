using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Hero.Command;

public class HeroController : MonoBehaviour
{
    public bool ground;
    public bool cooling;
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
        Debug.Log(Health);
        noRotation();
        hitCoolDown();

        if (Health <= 0)
        {
            playerDeath();
        }

        ground = false;
    }

    void playerDeath()
    {
        Destroy(this.gameObject);
    }       

    void noRotation()
    {
        if (this.transform.rotation != new Quaternion(0, 0, 0, 0))
        {
            this.transform.rotation = new Quaternion(0, 0, 0, 0);
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
        float xKnock = 7f;

        if (enemyLocation.x > this.transform.position.x)
        {
            xKnock = -7f;
        }
        var knockVector = new Vector2(xKnock, 7f);
        rb.velocity = knockVector;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            ground = true;
        }

        if ((collision.gameObject.tag == "enemy" || collision.gameObject.tag == "skeleton")
            && cooling == false)
        {
            playerHit(collision.gameObject);
            playerKnock(collision.gameObject);
        }
    }
}
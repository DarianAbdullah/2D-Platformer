using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonController : MonoBehaviour
{
    private float waitTime = 0.5f;
    private float counter = 0;
    private bool ground;
    private int health = 5;
    private Rigidbody2D SkeletonRigidBody;
    [SerializeField] GameObject player;
    [SerializeField] private float JumpStrength;
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
        counter = counter + Time.deltaTime;
        if (counter > waitTime)
        {
            var position = this.gameObject.transform.position;
            var heroPos = player.transform.position.x;
            float direction = 0f;
            if (heroPos > position.x)
            {
                direction = 1f;
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                direction = -1f;
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
            }
            position.x += direction * 2f * Time.deltaTime;
            this.gameObject.transform.position = position;
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            ground = true;
        }

        if (collision.gameObject.tag == "wall")
        {
            Debug.Log("hi");
            if (ground)
            {
                    SkeletonRigidBody.velocity += Vector2.up * this.JumpStrength;
            }
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

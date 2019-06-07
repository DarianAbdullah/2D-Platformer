using Hero.Command;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFireballAttack : MonoBehaviour, IHeroCommand
{
    [SerializeField]
    public GameObject FireballPrefab;
    private GameObject Player;
    private GameObject Fireball;
    private Rigidbody2D Rigidbody;
    private CircleCollider2D FireballCollider;
    private bool Active;
    private int Counter = 0;
    private const float DURATION = 4f;
    private float ElapsedTime;

    private const float VelocityX = 10.0f;
    private const float VelocityY = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        this.Active = false;
        this.ElapsedTime = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        Active = false;
        //if (Active)
        //{
        //    this.ElapsedTime += Time.deltaTime;
        //    if (!Player.GetComponent<SpriteRenderer>().flipX)
        //    {
        //        FireballCollider.enabled = true;
        //        var contacts = new Collider2D[6];
        //        this.FireballCollider.GetContacts(contacts);
        //        foreach (var col in contacts)
        //        {
        //            //Debug.Log(col.gameObject.tag);
        //            Counter += 1;
        //            if (Counter > 5)
        //            {
        //                Counter = 0;
        //                break;
        //            }
        //            if (col.gameObject.tag == "hero")
        //            {
        //                var doer = col.gameObject.GetComponent<HeroController>();
        //                doer.playerHit(Player);
        //                doer.playerKnock(Player);
        //                this.Active = false;
        //                Destroy(Fireball);
        //                return;
        //            }
        //            //break;
        //        }
        //        if (this.ElapsedTime > DURATION || !this.Active)
        //        {
        //            this.Active = false;
        //            FireballCollider.enabled = false;
        //        }
        //        return;
        //    }
        //}
    }

    public void Execute(GameObject gameObject)
    {
        if (!Active)
        {
            Active = true;
            Player = gameObject;
            var playerPosition = Player.transform.position;


                Fireball = (GameObject)Instantiate(FireballPrefab, new Vector3(playerPosition.x - 3f,
                    playerPosition.y - 2f, playerPosition.z), Player.transform.rotation);
                Rigidbody = Fireball.GetComponent<Rigidbody2D>();
                Rigidbody.velocity = new Vector2(-VelocityX, VelocityY);


            FireballCollider = Fireball.GetComponent<CircleCollider2D>();
            FireballCollider.enabled = true;
        }
    }
}

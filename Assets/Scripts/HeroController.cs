using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hero.Command;

public class HeroController : MonoBehaviour
{
    public IHeroCommand Right;
    public IHeroCommand Left;
    public bool ground;
    public bool cooling;
    public int health = 10;
    private float damageCoolDown = 1f;
    private float counter = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.rotation != new Quaternion(0, 0, 0, 0))
        {
            this.transform.rotation = new Quaternion(0, 0, 0, 0);
        }

        if (cooling)
        {
            counter += Time.deltaTime;
            if (counter > damageCoolDown)
            {
                cooling = false;
                counter = 0;
            }
        }
        Debug.Log(health);
        ground = false;
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
            health = health - 1;
            cooling = true;
        }
    }
}
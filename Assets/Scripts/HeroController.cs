using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hero.Command;

public class HeroController : MonoBehaviour
{
    public IHeroCommand Right;
    public IHeroCommand Left;
    public bool ground;
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

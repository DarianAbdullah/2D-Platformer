using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hero.Command;

public class HeroController : MonoBehaviour
{
    public IHeroCommand Right;
    public IHeroCommand Left;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Horizontal") > 0.01)
        {
            this.Right.Execute(this.gameObject);
        }
        else if (Input.GetAxis("Horizontal") < -0.01)
        {
            this.Left.Execute(this.gameObject);
        }
    }
}

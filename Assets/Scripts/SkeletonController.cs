using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonController : MonoBehaviour
{
    private float waitTime = 0.5f;
    private float counter = 0;
    [SerializeField] GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        counter = counter + Time.deltaTime;
        if (counter > waitTime)
        {
            var position = this.gameObject.transform.position;
            var heroPos = player.transform.position.x;
            float direction = 0f;
            if (heroPos > position.x)
            {
                direction = 1f;
            }
            else
            {
                direction = -1f;
            }
            position.x += direction * 2f * Time.deltaTime;
            this.gameObject.transform.position = position;
        }
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] GameObject target;
    private float origPos;
    private Camera Camera;
    // Start is called before the first frame update
    void Start()
    {
        Camera = this.gameObject.GetComponent<Camera>();
        origPos = Camera.transform.position.x;
    }
    void Update()
    {
        var camPos = Camera.transform.position;
        if (target != null)
        {
            
            {
                camPos.x = target.gameObject.transform.position.x;
            }
            
        }
        Camera.transform.position = camPos;
    }
}

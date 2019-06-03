using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] GameObject target;
    private Camera Camera;
    // Start is called before the first frame update
    void Start()
    {
        Camera = this.gameObject.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
     var camPos = Camera.transform.position;
     camPos.x = target.gameObject.transform.position.x;
     //camPos.y = target.gameObject.transform.position.y;
     Camera.transform.position = camPos;
    }
}

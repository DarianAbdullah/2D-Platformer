using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    //[SerializeField] private string level2;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("hI");
        if (collision.CompareTag("hero"))
        {
            SceneManager.LoadScene(6); 
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu1 : MonoBehaviour
{
    public int counter = 0;

    private void Start()
    {
        counter = 5;
    }
    private void Update()
    {
        if (Input.GetButtonDown("Submit") && counter > 0)
        {
            counter = 0;
            PlayGame();
        }
    }
    public void PlayGame()
    {
        Debug.Log(SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}

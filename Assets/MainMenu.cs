using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public int counter = 0;

    private void Start()
    {
        counter = 1;
    }
    private void Update()
    {
        if (Input.GetButtonDown("Submit") && counter == 1)
        {
            counter = 0;
            PlayGame();
        }
    }
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}

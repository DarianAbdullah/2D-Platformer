using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Restart : MonoBehaviour
{
    [SerializeField] private RectTransform GameOverScreen;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(IfGameOver())
        {
           if(Input.GetButtonDown("Submit")) 
           {
               RestartGame();
           }
        }
    }

    bool IfGameOver()
    {
        return GameOverScreen.gameObject.activeSelf;
    }

    // Loads the earlies levels
    public void RestartGame()
    {
        Application.LoadLevel(5);
    }
}

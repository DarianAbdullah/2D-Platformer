using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField] private RectTransform GameOverPanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Displays game over screen 
    public void IsGameOver()
    {
        GameOverPanel.gameObject.SetActive(true);
    }
}

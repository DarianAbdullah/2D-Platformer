using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Hero.Command;

public class HealthBar : MonoBehaviour
{
    // This script is meant to affect the health bar UI 

    private float CurrentHealth;
    private float MaxHealth;
    [SerializeField] private Slider MyHealthBar;
    [SerializeField] private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        MaxHealth = player.GetComponent<HeroController>().GetHealth();
        CurrentHealth = MaxHealth;
        MyHealthBar.value = CalculateHealthBar();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.X))
        {
            DealDamage(1);
        }
    }

    void DealDamage(float damage)
    {
        CurrentHealth -= damage;
        MyHealthBar.value = CalculateHealthBar();
    }

    float CalculateHealthBar()
    {
        return CurrentHealth / MaxHealth;
    }
}

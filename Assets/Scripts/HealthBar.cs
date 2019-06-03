using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    // This script is meant to affect the health bar UI 

    private float CurrentHealth;
    private float MaxHealth;
    [SerializeField] private Slider MyHealthBar;

    // Start is called before the first frame update
    void Start()
    {
        MaxHealth = 10;
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

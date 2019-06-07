using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Hero.Command;

public class MagicBar : MonoBehaviour
{
    // This script is meant to affect the Magic bar UI 

    private float Cooldown;
    private float Duration;
    [SerializeField] private Slider MyMagicBar;
    [SerializeField] private GameObject player;
    [SerializeField] private Image FireballSprite;
    [SerializeField] private Material ActiveMaterial;
    [SerializeField] private Material CooldownMaterial;
    // Start is called before the first frame update
    void Start()
    {
        FireballSprite.material = CooldownMaterial;
        Duration = player.GetComponent<FireballAttack>().GetDuration();
        Cooldown = Duration;
        MyMagicBar.value = CalculateMagicBar();
    }

    // Update is called once per frame
    void Update()
    {
        Cooldown = player.GetComponent<FireballAttack>().GetFireCooldown();
        if(Cooldown > Duration)
        {
            Cooldown = Duration;
        }
        MyMagicBar.value = CalculateMagicBar();

        if(MyMagicBar.value == 1f)
        {
            FireballSprite.material = ActiveMaterial;
        }

        else
        {
            FireballSprite.material = CooldownMaterial;
        }
    }

    float CalculateMagicBar()
    {
        return Cooldown / Duration;
    }
}

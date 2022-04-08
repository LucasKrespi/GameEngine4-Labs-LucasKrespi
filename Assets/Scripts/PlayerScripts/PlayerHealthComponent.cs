using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class PlayerHealthComponent : HealthComponent
{
    public Slider slider;

    protected override void Start()
    {
        base.Start();
        PlayerEvents.InvokeOnhealthInitializeEvent(this);
    }

    protected override void Update()
    {
        slider.value = CurrentHealth;
    }

    public void PotionHeal(int effect)
    {
        if (currentHealth < MaxHealth)
        {
            currentHealth = Mathf.Clamp(currentHealth + effect, 0, MaxHealth);
        }
    }
}

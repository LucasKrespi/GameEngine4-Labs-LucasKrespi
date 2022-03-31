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
}

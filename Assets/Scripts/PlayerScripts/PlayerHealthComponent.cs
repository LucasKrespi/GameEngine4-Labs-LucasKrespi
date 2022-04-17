using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



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

        if(currentHealth < 0)
        {
            PlayerPrefs.SetInt("Kills", GameManager.instance.zombiekills);
            AppEvents.InvokOnMouseCursorEnableEvent(true);
            SceneManager.LoadScene(2);
        }
    }

    public void PotionHeal(int effect)
    {
        if (currentHealth < MaxHealth)
        {
            currentHealth = Mathf.Clamp(currentHealth + effect, 0, MaxHealth);
        }
    }

    public override void Destroy()
    {
        //base.Destroy();
    }
}

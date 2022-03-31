using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEvents
{
    public delegate void OnWeaponEquippedEvent(WeaponComponent weaponComponent);

    public static event OnWeaponEquippedEvent OnWeaponEquipped;

    public static void InvokOnWeaponEquippedEvent(WeaponComponent weaponComponent)
    {
        OnWeaponEquipped?.Invoke(weaponComponent);
    }


    public delegate void OnhealthInitializeEvent(HealthComponent healthComponent);

    public static event OnhealthInitializeEvent OnhealthInitialize;

    public static void InvokeOnhealthInitializeEvent(HealthComponent healthComponent)
    {
        OnhealthInitialize?.Invoke(healthComponent);
    }


    //public delegate void OnTakeDamageEvent(float damage);

    //public static event OnTakeDamageEvent TakeDamage;

    //public static void InvokeOnTakeDamageEvent(float damage) 
    //{
    //    TakeDamage?.Invoke(damage);
    //}


    //public delegate void OnUSePotEvent(int life);

    //public static event OnUSePotEvent OnUsePot;

    //public static void InvokeOnOnUSePotEvent(int life)
    //{
    //    OnUsePot?.Invoke(life);
    //}
}

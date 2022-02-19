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


    public delegate void OnTakeDamageEvent(int damage);

    public static event OnTakeDamageEvent OnTakeDamage;

    public static void InvokeOnTakeDamageEvent(int damage)
    {
        OnTakeDamage?.Invoke(damage);
    }


    public delegate void OnUSePotEvent(int life);

    public static event OnUSePotEvent OnUsePot;

    public static void InvokeOnOnUSePotEvent(int life)
    {
        OnUsePot?.Invoke(life);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum WeaponType
{
    None,
    Pistol,
    Machinegun
}

public enum WeaponFiringPatterns
{
    SemiAuto,
    FullAuto,
    ThreeShotBurst
}

[System.Serializable]
public struct WeaponStats
{
    public WeaponType weaponType;
    public WeaponFiringPatterns firingPattern;
    public string waeponName;
    public float damage;
    public int bulletsInCLip;
    public int clipSize;
    public float fireStartDelay;
    public float fireRate;
    public float fireDistance;
    public bool repeating;
    public LayerMask weaponHitLayers;
}
public class WeaponComponent : MonoBehaviour
{
    public Transform gripLocationRight;
    public Transform gripLocationLeft;

    public WeaponStats weaponStats;

    protected WeaponHolder weaponHolder;

    public bool isFiring;
    public bool isRealoading;

    public Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Initialize(WeaponHolder _weaponHolder)
    {
        weaponHolder = _weaponHolder;
    }

    public virtual void StartFiringWeapon()
    {
        isFiring = true;
        if (weaponStats.repeating)
        {
            InvokeRepeating(nameof(FireWeapon), weaponStats.fireStartDelay, weaponStats.fireRate);
        }
        else
        {
            FireWeapon();
        }
    }

    public virtual void StopFiringWeapon()
    {
        isFiring = false;
        CancelInvoke(nameof(FireWeapon));
    }

    protected virtual void FireWeapon()
    {
        weaponStats.bulletsInCLip--;
    }
}

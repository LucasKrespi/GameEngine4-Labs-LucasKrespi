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
    public int totalBullets;
}
public class WeaponComponent : MonoBehaviour
{
    public Transform gripLocationRight;
    public Transform gripLocationLeft;

    public WeaponStats weaponStats;

    protected WeaponHolder weaponHolder;

    [SerializeField]
    protected ParticleSystem muzzeFlash;

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

    public void Initialize(WeaponHolder _weaponHolder, WeaponScript weaponScript)
    {
        weaponHolder = _weaponHolder;

        if (!weaponScript)
        {
            weaponStats = weaponScript.weaponStats;
        }
    }

    public virtual void StartFiringWeapon()
    {
        isFiring = true;
        if (weaponStats.repeating)
        {
            CancelInvoke(nameof(FireWeapon));
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

        if (muzzeFlash && muzzeFlash.isPlaying)
        {
            muzzeFlash.Stop();
        }
    }

    protected virtual void FireWeapon()
    {
        weaponStats.bulletsInCLip--;
    }


    public virtual void StartReloadindWeapon()
    {
        isRealoading = true;
        ReloadWeapon();
    }
    public virtual void ReloadWeapon()
    {
        if (muzzeFlash && muzzeFlash.isPlaying)
        {
            muzzeFlash.Stop();
        }

        int bulletsToReload = weaponStats.clipSize - weaponStats.totalBullets;
        if(bulletsToReload < 0)
        {
            int tempBullets = (weaponStats.clipSize - weaponStats.bulletsInCLip);
            weaponStats.bulletsInCLip += tempBullets;
            weaponStats.totalBullets -= tempBullets;
        }
        else
        {
            weaponStats.bulletsInCLip = weaponStats.totalBullets;
            weaponStats.totalBullets = 0;
        }

    }


    public virtual void StopReloading()
    {
        isRealoading = false;
    }

    public void SetExtendedMagSize()
    {
        weaponStats.clipSize += 10;
    }
}

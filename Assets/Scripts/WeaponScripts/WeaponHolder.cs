using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponHolder : MonoBehaviour
{
    [Header("Weapon to spawn"), SerializeField]
    GameObject weaponToSpawn;

    public PlayerController playerController;
    Animator playerAnimator;
    public Sprite crossHairImage;

    Transform gripIKTransformRight;
    Transform gripIKTransformLeft;
    WeaponComponent equippedWeapon;

    [SerializeField]
    GameObject weaponSocketLocation;

    public readonly int isFiringHash = Animator.StringToHash("isFiring");
    public readonly int isReloadingHash = Animator.StringToHash("isReloading");

    public bool isFirePressed;

    // Start is called before the first frame update
    void Start()
    {
        GameObject spawnedWeapon = Instantiate(weaponToSpawn, weaponSocketLocation.transform.position, weaponSocketLocation.transform.rotation, weaponSocketLocation.transform);
        playerController = GetComponent<PlayerController>();
        playerAnimator = GetComponent<Animator>();

        equippedWeapon = spawnedWeapon.GetComponent<WeaponComponent>();

        equippedWeapon.Initialize(this);

        PlayerEvents.InvokOnWeaponEquippedEvent(equippedWeapon);

        gripIKTransformRight = equippedWeapon.gripLocationRight;
        gripIKTransformLeft = equippedWeapon.gripLocationLeft;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnAnimatorIK(int layerIndex)
    {
        playerAnimator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
        playerAnimator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
        playerAnimator.SetIKPosition(AvatarIKGoal.RightHand, gripIKTransformRight.position);
        ////  playerAnimator.SetIKRotation(AvatarIKGoal.RightHand, gripIKTransformRight.rotation);

        if (!playerController.isReloading)
        {
            playerAnimator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
            playerAnimator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1);
            playerAnimator.SetIKPosition(AvatarIKGoal.LeftHand, gripIKTransformLeft.position);
            //playerAnimator.SetIKRotation(AvatarIKGoal.RightHand, gripIKTransformLeft.rotation);
        }
    }
    public void OnFire(InputValue value)
    {
        isFirePressed = value.isPressed;
        if (isFirePressed)
        {
            StartFiring();
        }
        else
        {
            StopFiring();
        }

    }

    private void StartFiring()
    {
        if (equippedWeapon.weaponStats.bulletsInCLip <= 0)
        {
            OnStartReload();
            return;
        }

        playerAnimator.SetBool(isFiringHash, true);
        playerController.isFiring = true;
        equippedWeapon.StartFiringWeapon();

    }

    private void StopFiring()
    {
        playerAnimator.SetBool(isFiringHash, false);
        playerController.isFiring = false;
        equippedWeapon.StopFiringWeapon();
    }
    public void OnReload(InputValue value)
    {

        playerController.isReloading = value.isPressed;
        OnStartReload();

    }

    public void OnStartReload()
    {
        if (playerController.isFiring)
        {
            StopFiring();
        }
        if (equippedWeapon.weaponStats.totalBullets <= 0)
        {
            return;
        }


        equippedWeapon.StartReloadindWeapon();
        playerAnimator.SetBool(isReloadingHash, true);
        playerController.isReloading = true;
        InvokeRepeating(nameof(OnStopReload), 0.0f, 0.1f);
    }
    public void OnStopReload()
    {
        if (playerAnimator.GetBool(isReloadingHash)) return;

        playerAnimator.SetBool(isReloadingHash, false);
        equippedWeapon.StopReloading();
        playerController.isReloading = false;
        CancelInvoke(nameof(OnStopReload));

        if (isFirePressed)
        {
            StartFiring();
        }
    }

    public WeaponComponent ReturnEquippedWeapon()
    {
        return equippedWeapon;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public bool isFiring;
    public bool isReloading;
    public bool isJumping;
    public bool isRunning;
    public bool isAiming;

    public bool inInventory;
    public InventoryComponent inventory;
    public GameUIController uiController;

    public WeaponHolder weaponHolder;
    public HealthComponent playerHealth;

    private void Awake()
    {
        uiController = FindObjectOfType<GameUIController>();
        inventory = GetComponent<InventoryComponent>();
        weaponHolder = GetComponent<WeaponHolder>();
        playerHealth = GetComponent<HealthComponent>();

        AppEvents.InvokOnMouseCursorEnableEvent(false);

    }

    public void OnInventory(InputValue valeu)
    {
        if (inInventory)
        {
            inInventory = false;
            AppEvents.InvokOnMouseCursorEnableEvent(false);
        }
        else
        {
            inInventory = true;
            AppEvents.InvokOnMouseCursorEnableEvent(true);
        }
        OpenInventory(inInventory);
    }

    private void OpenInventory(bool open)
    {
        if (open)
        {
            uiController.EnableInventoryMenu();
        }
        else
        {
            uiController.EnableGameMenu();
        }
    }
}

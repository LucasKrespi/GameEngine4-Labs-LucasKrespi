using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Itens/Weapon", order = 2)]
public class WeaponScript : EqquipableScript
{
    public WeaponStats weaponStats;

    public override void UseItem(PlayerController playerController)
    {
        if (equipped)
        {

        }
        else
        {
            playerController.weaponHolder.equippeWeapon(this);
           //PlayerEvents.InvokOnWeaponEquippedEvent(itemPrefab.GetComponent<WeaponComponent>());
        }

        base.UseItem(playerController);
    }
}

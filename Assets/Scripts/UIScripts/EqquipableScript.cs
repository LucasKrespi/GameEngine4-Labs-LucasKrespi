using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EqquipableScript : ItemScript
{
    public bool equipped
    {
        get => isEquipped;
        set
        {
            isEquipped = value;
            OnequipedStatusChange?.Invoke();
        }
    }
    public bool isEquipped;
    
    public delegate void EquipStatusChange();
    public event EquipStatusChange OnequipedStatusChange;

    public override void UseItem(PlayerController playerController)
    {
        isEquipped = !isEquipped;
    }
}

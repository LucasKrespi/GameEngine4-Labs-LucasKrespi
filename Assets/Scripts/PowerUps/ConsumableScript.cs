using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Consumable", menuName = "Itens/Consumable", order = 1)]
public class ConsumableScript : ItemScript
{
    public int effect = 0;
    public override void UseItem(PlayerController playerController)
    {
        SetAmount(amountValue -1);

        if(amountValue <= 0)
        {
            DeleteItem(playerController);
        }
    }
}

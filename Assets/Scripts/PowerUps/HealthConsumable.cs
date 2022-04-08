using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HealthPotion", menuName = "Itens/HealthPotion", order = 1)]
public class HealthConsumable : ConsumableScript
{
    public override void UseItem(PlayerController playerController)
    {
        if(playerController.playerHealth.CurrentHealth < playerController.playerHealth.MaxHealth)
        {
            playerController.playerHealth.TakeDamage(-this.effect);
      
        }
        base.UseItem(playerController);
    }
}

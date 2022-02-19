using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    public Slider healthbar;

    public void updateHealthBarValeu(int health)
    {
        healthbar.value = health;
    }
}

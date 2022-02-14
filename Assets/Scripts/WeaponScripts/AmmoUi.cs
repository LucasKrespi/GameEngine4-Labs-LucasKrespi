using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AmmoUi : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI weaponNameText;
    [SerializeField]
    TextMeshProUGUI ammoInClipText;
    [SerializeField]
    TextMeshProUGUI ammoTotalText;

    [SerializeField]
    WeaponComponent weaponComponent;

    private void OnEnable()
    {
        PlayerEvents.OnWeaponEquipped += OnWeaponEquipped;
    }

    private void OnDisable()
    {
        PlayerEvents.OnWeaponEquipped -= OnWeaponEquipped;
    }

    void OnWeaponEquipped(WeaponComponent weaponComponent)
    {
        this.weaponComponent = weaponComponent;
    }

    private void Update()
    {
        if (!weaponComponent) return;

        weaponNameText.text = weaponComponent.weaponStats.waeponName;
        ammoInClipText.text = weaponComponent.weaponStats.bulletsInCLip.ToString();
        ammoTotalText.text = weaponComponent.weaponStats.totalBullets.ToString();
    }
}

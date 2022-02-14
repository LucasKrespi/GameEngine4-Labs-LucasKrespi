using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AK47Component : WeaponComponent
{
    protected override void FireWeapon()
    {
        Vector3 hitLocation;

        if (muzzeFlash)
        {
            muzzeFlash.Play();
        }

        if(weaponStats.bulletsInCLip > 0 && !isRealoading)
        {
            base.FireWeapon();
            Ray screenRay = mainCamera.ViewportPointToRay(new Vector2(0.5f, 0.5f));

            if(Physics.Raycast(screenRay, out RaycastHit hit, weaponStats.fireDistance, weaponStats.weaponHitLayers))
            {
                hitLocation = hit.point;
                Vector3 hitDirection = hit.point - mainCamera.transform.position;

                Debug.DrawRay(mainCamera.transform.position, hitDirection.normalized * weaponStats.fireDistance, Color.red, 1);
            }
        }
        else if (weaponStats.bulletsInCLip <= 0)
        {
            weaponHolder.OnStartReload();
        }
    }
}

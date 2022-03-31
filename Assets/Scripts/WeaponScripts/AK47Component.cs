using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AK47Component : WeaponComponent
{
    Vector3 hitLocation;
    protected override void FireWeapon()
    {
      

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

                DealDamage(hit);

               //Debug.DrawRay(mainCamera.transform.position, hitDirection.normalized * weaponStats.fireDistance, Color.red, 1);
            }
        }
        else if (weaponStats.bulletsInCLip <= 0)
        {
            weaponHolder.OnStartReload();
        }
    }

    public void DealDamage(RaycastHit hitinfo)
    {
        IDamageble damageble = hitinfo.collider.GetComponent<IDamageble>();
        damageble?.TakeDamage(weaponStats.damage);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(hitLocation, 0.1f);
    }
}  

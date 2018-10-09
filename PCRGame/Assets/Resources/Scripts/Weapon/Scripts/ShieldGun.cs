using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldGun : BaseWeapon {
    private void Awake()
    {
        weaponName = "ShieldGun";
        fireRate = 50;
        damage = 0;
        shotSpeed = 0.0f;
        heatValue = 2;
    }

    public override void Fire()
    {
        mechInterface = GetComponentInParent<MechBase>();
        if (!firing)
        {
            mechInterface.BuildHeat(heatValue);

            Instantiate(projectile, transform.parent.transform.position, firePoint.transform.rotation, transform.parent.transform);
            firing = true;
            mechInterface.SetShielded(true);
        }
    }
}

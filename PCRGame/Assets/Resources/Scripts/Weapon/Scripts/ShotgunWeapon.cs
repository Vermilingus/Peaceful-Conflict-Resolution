using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunWeapon : BaseWeapon {
    int pellets = 8;
    
    private void Awake()
    {
        weaponName = "Shotgun";
        fireRate = 3;
        damage = 25;
        shotSpeed = 0.2f;
        heatValue = 10;
        spread = 30;
    }

    public override void Fire()
    {
        mechInterface = GetComponentInParent<MechBase>();
        if (!firing)
        {
            launchShot();
            mechInterface.BuildHeat(heatValue);
            firing = true;

            for(int i = 0; i<pellets;i++)
            {
                var deviation = Random.Range(-spread, spread);
                var pellet = Instantiate(projectile, firePoint.transform.position, firePoint.transform.rotation);
                pellet.transform.Rotate(0, 0, deviation);
                projectile.GetComponent<BaseProjectile>().SetDamage(damage);
                projectile.GetComponent<BaseProjectile>().SetShotSpeed(shotSpeed+Random.Range(0,0.05f));
            }
        }
    }
}

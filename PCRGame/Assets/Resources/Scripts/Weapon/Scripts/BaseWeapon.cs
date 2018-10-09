using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseWeapon : MonoBehaviour {

    protected string weaponName;
    protected float fireRate;
    protected int damage;

    public GameObject projectile;

    protected bool firing;
    protected float fireTimer;
    protected float fireDelay;
    protected float shotSpeed;

    protected IMech mechInterface;

    protected float heatValue;

    protected GameObject firePoint;
    protected float spread;

    public virtual void Fire()
    {
        mechInterface = GetComponentInParent<MechBase>();
        if (!firing)
        {
            launchShot();
            mechInterface.BuildHeat(heatValue);
            firing = true;
        }  
    }

    public void setFirePoint(GameObject fp)
    {
        firePoint = fp;
    }

    protected virtual void launchShot()
    {
        float deviation = Random.Range(-spread, spread);

        var pellet = Instantiate(projectile, firePoint.transform.position, firePoint.transform.rotation);
        pellet.GetComponent<BaseProjectile>().SetDamage(damage);
        pellet.GetComponent<BaseProjectile>().SetShotSpeed(shotSpeed);
        pellet.transform.Rotate(0, 0, deviation);
    }

    public bool isFiring()
    {
        return firing;
    }

    public string GetWeaponName()
    {
        return weaponName;
    }

    public void SetFireRate(float newFR)
    {
        fireRate = newFR;
    }

    public float GetFireRate()
    {
        return fireRate;
    }

    public int GetDamage()
    {
        return damage;
    }

    private void Update()
    {
        fireDelay = 1 / fireRate;
        if (firing == true)
        {
            fireTimer += Time.deltaTime;

            if (fireTimer >= fireDelay)
            {
                firing = false;
                fireTimer = 0;
            }
        }
    }

}

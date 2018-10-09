using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VanillaMG : BaseWeapon {

    private void Awake()
    {
        weaponName = "MachineGun";
        fireRate = 10;
        damage = 10;
        shotSpeed = 0.3f;
        heatValue = 2;
    }
}

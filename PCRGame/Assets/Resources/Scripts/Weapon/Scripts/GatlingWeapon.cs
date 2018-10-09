using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatlingWeapon : BaseWeapon {

    private void Awake()
    {
        weaponName = "Gatling Gun";
        fireRate = 2.0f;
        damage = 4;
        shotSpeed = 0.4f;
        heatValue = 5;
        spread = 10;
    }
}
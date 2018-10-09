using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechGatling : MechBase {

    public override void initMech()
    {
        maxHP = 75;
        SetHP(maxHP);
        mechName = "Gatling";
        rb = this.GetComponent<Rigidbody2D>();
        maxSpeed = 3;
        DashMod = 5;
        DashDuration = 0.1f;
        moveForce = 50;
        turnSpeed = 2;
        maxHeat = 200;
        heatedThreshold = 20;
        heatBonus = 3;
        cooldownRate = 100;
        DashHeat = 10;
        print(GetHP());
    }

    public override void FirePrimary()
    {
        if (!GetOverheat())
        {
            PrimaryWeapon.Fire();
            SecondaryWeapon.Fire();

            PrimaryWeapon.SetFireRate(PFireRate * (((GetHeat() / GetMaxHeat()) + 1))*5);
            SecondaryWeapon.SetFireRate(PFireRate * (((GetHeat() / GetMaxHeat()) + 1))*5);
        }

    }

    public override void FireSecondary()
    {
        if (!GetOverheat())
        {
            SecondaryWeapon.Fire();
            PrimaryWeapon.Fire();

            PrimaryWeapon.SetFireRate(PFireRate * (((GetHeat() / GetMaxHeat()) + 1)) * 10);
            SecondaryWeapon.SetFireRate(PFireRate * (((GetHeat() / GetMaxHeat()) + 1)) * 10);
        }

    }
}
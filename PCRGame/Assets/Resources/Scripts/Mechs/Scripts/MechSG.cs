using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechSG : MechBase {

    public override void initMech()
    {
        maxHP = 125;
        SetHP(maxHP);
        mechName = "Patrol";
        rb = this.GetComponent<Rigidbody2D>();
        maxSpeed = 4;
        DashMod = 10;
        DashDuration = 0.2f;
        moveForce = 45;
        turnSpeed = 7.5f;
        maxHeat = 150;
        heatedThreshold = 66.6f;
        heatBonus = 2;
        cooldownRate = 75;
        DashHeat = 60;
        print(GetHP());
    }
}

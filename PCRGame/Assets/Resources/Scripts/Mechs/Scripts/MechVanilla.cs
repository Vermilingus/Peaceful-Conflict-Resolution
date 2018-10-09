using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechVanilla : MechBase {

    public override void initMech()
    {
        maxHP = 100;
        SetHP(maxHP);
        mechName = "Vanilla";
        rb = this.GetComponent<Rigidbody2D>();
        maxSpeed = 5;
        DashMod = 20;
        DashDuration = 0.1f;
        moveForce = 50;
        turnSpeed = 20;
        maxHeat = 100;
        heatedThreshold = 50;
        heatBonus = 3;
        cooldownRate = 50;
        DashHeat = 40;
        print(GetHP());
    }
    
}
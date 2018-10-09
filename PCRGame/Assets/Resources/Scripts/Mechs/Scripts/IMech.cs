using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMech
{
    void TakeDamage(int damage);

    void UpdateMovement(float xDir, float yDir, float rotX, float rotY,float deadzone);

    void FirePrimary();
    void FireSecondary();

    void BuildHeat(float heat);
    float GetHeat();
    float GetMaxHeat();

    int GetHP();
    int GetMaxHP();
    void SetHP(int newHP);
    void SetShielded(bool newShielded);
    void ReturnToSpawn();

    void initMech();
    void initWeapons();
    void setUI(GameObject UI);

    void Dash();

    void ChangeSafety(bool NewIsSafe);
    void startListening();
}
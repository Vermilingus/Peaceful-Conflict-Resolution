using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class MechGameManager : MonoBehaviour {
    public static MechGameManager instance = null;
    string Mech0;
    string Mech1;
    string Mech2;
    string Mech3;

    string CurrentLevel;

    int MechsPresent;
    int MechsRemaining;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (instance == null)
        {
            instance = this;

        }
            

        else if (instance != this)
        {
            Destroy(gameObject);
        }
            
    }

    public void ChangeMechNumber(int mod)
    {
        MechsRemaining += mod;
        print(MechsRemaining + " Mechs remaining");

        if (MechsRemaining <= 1)
        { ReturnAllToSpawn(); }

    }

    public void MechDeath()
    {
        MechsPresent--;

        if(MechsPresent <= 1)
        {
            EndGame();
        }
    }

    public void ChangeMech(int player, string chosenMech)
    {
        switch(player)
        {
            case 0:
                Mech0 = chosenMech;
                print("Mech " + player + " : " + Mech0);
                break;
            case 1:
                Mech1 = chosenMech;
                print("Mech " + player + " : " + Mech1);
                break;
            case 2:
                Mech2 = chosenMech;
                print("Mech " + player + " : " + Mech2);
                break;
            case 3:
                Mech3 = chosenMech;
                print("Mech " + player + " : " + Mech3);
                break;
        }

    }

    public string returnMech(int player)
    {
        switch (player)
        {
            case 0:
                print("Mech " + player + " : " + Mech0);
                return Mech0;
            case 1:
                print("Mech " + player + " : " + Mech1);
                return Mech1;
            case 2:
                print("Mech " + player + " : " + Mech2);
                return Mech2;
            case 3:
                print("Mech " + player + " : " + Mech3);
                return Mech3;
            default:
                return null;
        }
    }

    public void ChangeLevel(string chosenLevel)
    {
        CurrentLevel = chosenLevel;
        print(CurrentLevel);
    }

    public void startGame()
    {
        if (CurrentLevel == null)
        {
            ChangeLevel("BaseLevel");
        }
        SceneManager.LoadScene(CurrentLevel);
        MechsRemaining = MechsPresent;
    }

    public void AddMech()
    {
        MechsPresent += 1;
        print(MechsPresent);
        MechsRemaining = MechsPresent;
    }

    public void ReturnAllToSpawn()
    {
        print("Return");
        EventManager.TriggerEvent("ReturnToSpawn");
        MechsRemaining = MechsPresent;
    }

    public void EndGame()
    {
        MechsPresent = 0;
        SceneManager.LoadScene("main");
    }
}

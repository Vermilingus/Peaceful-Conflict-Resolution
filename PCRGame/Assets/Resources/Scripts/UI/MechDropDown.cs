using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MechDropDown : MonoBehaviour {
    public int player;
    protected string mechName;
    private int value;

    protected Dropdown thisDropdown;

    // Use this for initialization
    void Start () {
        thisDropdown = transform.GetComponent<Dropdown>();
        MechGameManager.instance.ChangeMech(player, "MechVanilla");
    }
	
	// Update is called once per frame
	public void Change () {
        value = thisDropdown.value;

        switch (value)
        {
            case 0:
                MechGameManager.instance.ChangeMech(player, "MechVanilla");
                break;
            case 1:
                MechGameManager.instance.ChangeMech(player, "MechPatrol");
                break;
            case 2:
                MechGameManager.instance.ChangeMech(player, "MechGatling");
                break;
            default:
                MechGameManager.instance.ChangeMech(player, "MechVanilla");
                break;
        }
    }
}

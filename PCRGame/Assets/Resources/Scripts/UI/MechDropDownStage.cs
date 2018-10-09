using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MechDropDownStage : MonoBehaviour
{
    private int value;

    protected Dropdown thisDropdown;

    // Use this for initialization
    void Start()
    {
        thisDropdown = transform.GetComponent<Dropdown>();
        MechGameManager.instance.ChangeLevel("Level1v1");
    }

    // Update is called once per frame
    public void Change()
    {
        value = thisDropdown.value;

        switch (value)
        {
            case 0:
                MechGameManager.instance.ChangeLevel("Level1v1");
                break;
            case 1:
                MechGameManager.instance.ChangeLevel("BaseLevel");
                break;
            default:
                MechGameManager.instance.ChangeLevel("Level1v1");
                break;
        }
    }
}

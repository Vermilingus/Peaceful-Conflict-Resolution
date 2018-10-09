using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadMechButton : MonoBehaviour {
    public int player;
    public string mechName;

    private void Start()
    {
        Button thisButton = transform.GetComponent<Button>();
    }

    public void ButtonPress()
    {
        MechGameManager.instance.ChangeMech(player, mechName);
    }
}

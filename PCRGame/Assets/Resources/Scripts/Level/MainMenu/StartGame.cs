using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGame : MonoBehaviour {

    private void Start()
    {
        Button thisButton = transform.GetComponent<Button>();
    }

    public void ButtonPress()
    {
        MechGameManager.instance.startGame();
    }
}

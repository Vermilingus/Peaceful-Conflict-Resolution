using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour {
    private void Awake()
    {
        int count = transform.childCount;
        for(int i = 0; i < count; i++)
        {
            transform.GetChild(i).GetComponent<MechController>().SetPlayer(i);
            transform.GetChild(i).GetComponent<MechController>().SetMech(i);
            transform.GetChild(i).GetComponent<MechController>().initMech();
        }
    }
}

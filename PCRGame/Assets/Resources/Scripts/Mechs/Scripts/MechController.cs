using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MechController : MonoBehaviour
{
    private float xDir;
    private float yDir;
    private float rotX;
    private float rotY;
    float deadzone = 0.25f;

    GameObject controlledMech;
    private GameObject mech;
    public IMech mechActor;

    protected GameObject ui;

    private int playerNum;
    string mechName;

    private int mechMaxHP;
    private int mechHP;

    public SafeZone pcSafeZone;

    public void SetPlayer(int player)
    {
        playerNum = player;
    }

    public void SetMech(int player)
    {
        mechName = MechGameManager.instance.returnMech(player);
    }

    public void initMech()
    {
        if (MechGameManager.instance.returnMech(playerNum) != null)
        {
            string mechpath = ("Scripts/Mechs/" + MechGameManager.instance.returnMech(playerNum));
            print(mechpath);
            controlledMech = Resources.Load(mechpath) as GameObject;
            mech = Instantiate(controlledMech, transform.position, transform.rotation);
            print("Mech loaded correctly");
            mechActor = mech.GetComponent<MechBase>();
            mechActor.initMech();
            print("Mech stats initialised");
            mechActor.initWeapons();
            print("Mech weapons initialised");
            ui = transform.GetChild(0).gameObject;
            mechActor.setUI(ui);
            print("Mech UI initialised");
            MechGameManager.instance.AddMech();
        }
    }


    // Update is called once per frame
    void Update()
    {
        if(mechActor != null)
        {
            xDir = Input.GetAxis("XDirection" + playerNum);
            yDir = Input.GetAxis("YDirection" + playerNum);
            rotX = Input.GetAxis("RotationX" + playerNum);
            rotY = Input.GetAxis("RotationY" + playerNum);

            

            if (Input.GetButton("FirePrimary" + playerNum))
            {
                mechActor.FirePrimary();
            }

            if (Input.GetButton("FireSecondary" + playerNum))
            {
                mechActor.FireSecondary();
            }

            if (Input.GetButtonDown("Dash" + playerNum))
            {
                mechActor.Dash();
            }

            mechActor.UpdateMovement(xDir, yDir, rotX, rotY, deadzone);

        }    
    }
}

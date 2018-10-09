using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;


public abstract class MechBase : MonoBehaviour, IMech {

    SafeZone safeZone;

    //Initialises variables
    protected string mechName;
    protected int mechHP;
    protected int maxHP;

    protected int maxSpeed;
    protected int DashMod;
    protected int moveForce;
    protected float turnSpeed;
    protected float DashDuration;
    private float DashTimer;
    public bool Dashing;
    protected float DashHeat;

    protected Rigidbody2D rb;

    protected bool DangerState = false;

    protected BaseWeapon PrimaryWeapon;
    protected BaseWeapon SecondaryWeapon;
    protected float PFireRate;
    protected float SFireRate;

    private bool iFrames = false;
    private float iDuration = 0.2f;
    private float iCount = 0.0f;

    protected float maxHeat;
    private float currentHeat;
    protected float cooldownRate;
    private bool overHeat = false;
    private float heatMod = 1;
    protected float heatBonus;
    protected float heatedThreshold;

    protected GameObject MechUI;
    protected Slider heatUI;
    protected Slider HPUI;

    protected bool isMoving;
    protected bool isShielded;

    public abstract void initMech();

    private bool dead;
    private bool isSafe;

    private Vector3 SpawnPoint;
    private int MechLives = 3;

    private UnityAction listener;

    public Vector3 GetSpawnPoint()
    {
        return SpawnPoint;
    }

    public int GetLives()
    {
        return MechLives;
    }

    public void ModifyLives(int newLives)
    {
        MechLives += newLives;
    }

    public void Awake()
    {
        listener = new UnityAction(ReturnToSpawn);
        startListening();
    }

    public void initWeapons()
    {
        PrimaryWeapon = transform.GetChild(0).GetComponent<BaseWeapon>();
        SecondaryWeapon = transform.GetChild(1).GetComponent<BaseWeapon>();

        PrimaryWeapon.setFirePoint(PrimaryWeapon.transform.GetChild(0).gameObject);
        SecondaryWeapon.setFirePoint(SecondaryWeapon.transform.GetChild(0).gameObject);

        PFireRate = PrimaryWeapon.GetFireRate();
        SFireRate = SecondaryWeapon.GetFireRate();

        SpawnPoint = transform.position;
        MechLives = 3;
        print(mechName + " " + MechLives);
    }

    public void startListening()
    {
        EventManager.StartListening("ReturnToSpawn", listener);
    }

    public void ReturnToSpawn()
    {
        if(MechLives>0)
        {
        MechUI.SetActive(true);
        gameObject.SetActive(true);
        transform.position = GetSpawnPoint();
        SetHP(GetMaxHP());
        BuildHeat(-1000000);
        DashTimer = 3;

        UpdateHPUI();

        dead = false;
        DangerState = false;

        print(isActiveAndEnabled);
        }        
    }

    public void setUI(GameObject UI)
    {
        MechUI = UI;

        heatUI = MechUI.transform.GetChild(0).GetComponent<Slider>();
        HPUI = MechUI.transform.GetChild(1).GetComponent<Slider>();
        HPUI.maxValue = GetMaxHP();
        heatUI.maxValue = maxHeat;

        MechUI.GetComponent<FollowPlayer>().target = transform;

        UpdateHPUI();
    }

    //return functions to retain data privacy
    public string GetMechName()
    {
        return mechName;
    }

    public int GetHP()
    {
        return mechHP;
    }

    public int GetMaxHP()
    {
        return maxHP;
    }

    public void SetHP(int newHP)
    {
        mechHP = newHP;
    }

    public float GetHeat()
    {
        return currentHeat;
    }

    public float GetMaxHeat()
    {
        return maxHeat;
    }

    public bool GetOverheat()
    {
        return overHeat;
    }

    //damage dealing function


    //kills player, broadcasts death to world
    public void Kill()
    {
        ModifyLives(-1);
        if (MechLives > 0)
        {
            print(mechName + " " + MechLives);
            DangerState = false;

            
            dead = true;
            MechUI.SetActive(false);
            gameObject.SetActive(false);
            MechGameManager.instance.ChangeMechNumber(-1);
        }
        else
        {
            
            dead = true;
            MechUI.SetActive(false);
            gameObject.SetActive(false);
            MechGameManager.instance.MechDeath();
        }
        

    }

    public void BuildHeat(float heat)
    {
        if(!overHeat)
        {
            currentHeat += heat;
            if (currentHeat < 0)
            {
                currentHeat = 0;
            }
            else if (currentHeat > GetMaxHeat())
            {
                currentHeat = GetMaxHeat();
            }
        }
        

        UpdateHeatUI();
    }

    public void UpdateHeatUI()
    {
        heatUI.value = GetHeat();

        if (currentHeat >= maxHeat)
        {
            overHeat = true;
        }

        if ((currentHeat / maxHeat) * 100 > heatedThreshold)
        {
            heatMod = (currentHeat / maxHeat) * heatBonus;
        }
        else
        {
            heatMod = 1;
        }
    }

    public void UpdateHPUI()
    {
        HPUI.value = GetHP();
    }

    public void UpdateMovement(float xDir,float yDir, float rotX, float rotY, float deadzone)
    {
        isMoving = false;
        iFrames = false;

        if (!overHeat && !dead)
        {
            if (xDir * rb.velocity.x < maxSpeed*heatMod)
            {
                rb.AddForce(xDir * Vector2.right * moveForce * heatMod);
                isMoving = true;
            }

            if (Mathf.Abs(rb.velocity.x) > maxSpeed * heatMod)
            {
                rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxSpeed * heatMod, rb.velocity.y);
                isMoving = true;
            }

            if (yDir * rb.velocity.y < maxSpeed * heatMod)
            {
                rb.AddForce(yDir * Vector2.up * moveForce * heatMod);
                isMoving = true;
            }

            if (Mathf.Abs(rb.velocity.y) > maxSpeed * heatMod)
            {
                rb.velocity = new Vector2(rb.velocity.x, Mathf.Sign(rb.velocity.y) * maxSpeed * heatMod);
                isMoving = true;
            }

            Vector3 direction = new Vector3(rotX, rotY, 0);
            if (direction.magnitude >= deadzone)
            {
                Quaternion currentRotation = Quaternion.LookRotation(Vector3.forward, direction);
                transform.rotation = Quaternion.Lerp(transform.rotation, currentRotation, Time.deltaTime * turnSpeed * heatMod);
            }

            if (!((xDir >= deadzone || xDir <= -deadzone) || (yDir >= deadzone || yDir <= -deadzone)))
            {
                rb.velocity = new Vector2(0,0);
                isMoving = false;
            }
        }

        if (PrimaryWeapon.isFiring() == false && SecondaryWeapon.isFiring() == false && !dead)
        {
            if(!Dashing)
            {
                if (currentHeat > 0)
                {
                    currentHeat -= cooldownRate * Time.deltaTime;
                    UpdateHeatUI();
                }
                else
                {
                    currentHeat = 0;
                    overHeat = false;
                    UpdateHeatUI();
                }
                isShielded = false;
            }   
        }

        
    }

    private void FixedUpdate()
    {

        if(DashTimer>0)
        {
            DashTimer -= Time.deltaTime;
            iFrames = true;
        }
        else if (Dashing)
        {
            iFrames = false;
            Dashing = false;
            EndDash();
        }
    }

    public virtual void FirePrimary()
    {
        if (!overHeat && !dead && !isSafe && !isShielded)
        {
            PrimaryWeapon.Fire();
        }
        
    }


    public virtual void FireSecondary()
    {
        if (!overHeat && !dead && !isSafe)
        {
            SecondaryWeapon.Fire();
        }
        
    }

    public void Dash()
    {
        if(!Dashing && !dead && isMoving)
        {
            DashTimer = DashDuration;
            moveForce = moveForce * DashMod;
            maxSpeed = maxSpeed * DashMod;
            Dashing = true;
            BuildHeat(DashHeat);
        }
    }

    private void EndDash()
    {
        moveForce = moveForce / DashMod;
        maxSpeed = maxSpeed / DashMod;
        Dashing = false;
    }

    public void TakeDamage(int damage)
    {
        print("Start HP" + GetHP());
        print(iFrames);
        if(!iFrames && !dead && !isSafe && !isShielded)
        {
            if (DangerState)
            {
                Kill();
            }
            else
            {
                mechHP -= damage;

                if (GetHP() <= 0)
                {
                    DangerState = true;
                }
                print("Damage" + " " + damage + " End HP " + GetHP());
                print(DangerState);
            }

        }

        UpdateHPUI();
        
    }

    public void ChangeSafety(bool NewIsSafe)
    {
        isSafe = NewIsSafe;
    }

    public void SetShielded(bool newShielded)
    {
        isShielded = newShielded;
    }
}

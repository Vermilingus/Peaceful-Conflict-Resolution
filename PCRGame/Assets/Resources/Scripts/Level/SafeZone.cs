using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class SafeZoneActivateEventArgs : EventArgs
{
    public float safeTime;

    public SafeZoneActivateEventArgs(float safeTime)
    {
        this.safeTime = safeTime;
    }
}

public class SafeZone : MonoBehaviour
{
    public delegate void SafeZoneEventDispatcher(object sender, SafeZoneActivateEventArgs args);
    public static event SafeZoneEventDispatcher OnSafeZoneActivate;

    public float aliveTimer = 3;

    public bool isActive;

    IMech mech;

    void Start()
    {
        OnSafeZoneActivate += activate;
        
    }

    public void Update()
    {
        if(isActive)
        {
            aliveTimer -= 1.0f * Time.deltaTime;

            if (aliveTimer <= 0)
            {
                isActive = false;
            }
        }
        else
        {
            transform.GetComponent<SpriteRenderer>().enabled = false;

            if (mech != null)
            {
                mech.ChangeSafety(false);
            }
        }
    
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(isActive)
        {
            if (other.transform.CompareTag("Player"))
            {
                mech = other.gameObject.GetComponent<MechBase>();
                mech.ChangeSafety(true);
            }
            else if(other.transform.CompareTag("Projectile"))
            {
                Destroy(other.gameObject);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            mech = other.gameObject.GetComponent<MechBase>();
            mech.ChangeSafety(false);
        }
    }

    public static void Call_SafeZoneActivate(object sender, SafeZoneActivateEventArgs args)
    {
        if (OnSafeZoneActivate != null)
        {
            OnSafeZoneActivate(sender, args);
        }
    }

    public void activate(object sender, SafeZoneActivateEventArgs args)
    {
        isActive = true;
        aliveTimer = args.safeTime;
        transform.GetComponent<SpriteRenderer>().enabled = true;
    }
}

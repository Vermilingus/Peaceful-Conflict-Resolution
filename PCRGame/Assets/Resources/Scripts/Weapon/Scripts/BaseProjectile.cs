using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseProjectile : MonoBehaviour {
    protected int damage;

    protected float decayTime;
    protected float range = 5.0f;

    public float shotSpeed;

    public void SetShotSpeed(float newSpeed)
    {
        shotSpeed = newSpeed;
    }

    private void Awake()
    {
        decayTime = 0;
    }

    // Update is called once per frame
    void FixedUpdate () {
        decayTime += 0.1f;
        if(decayTime >= range)
        {
            Destroy(gameObject);
        }

        transform.position = new Vector3(transform.position.x + (transform.up * shotSpeed).x, transform.position.y + (transform.up * shotSpeed).y, transform.position.z);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.transform.CompareTag("Player"))
        {
            IMech mech = other.gameObject.GetComponent<MechBase>();
            mech.TakeDamage(damage);
            enabled = false;            
        }

        if (!(other.transform.CompareTag("SafeZone")))
        {
            Destroy(this.gameObject);
        }

        
    }

    public float GetDamage()
    {
        return damage;
    }

    public void SetDamage(int newDamage)
    {
        damage = newDamage;
    }
}

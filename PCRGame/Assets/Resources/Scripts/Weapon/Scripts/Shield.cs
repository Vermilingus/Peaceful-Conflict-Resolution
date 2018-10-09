using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : BaseProjectile {
    private void Awake()
    {
        range = 0.2f;
    }

    private void FixedUpdate()
    {
        decayTime += 0.1f;
        if (decayTime >= range)
        {
            Destroy(gameObject);
        }

        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.transform.CompareTag("Projectile"))
        {
            other.enabled = false;
        }
    }
}

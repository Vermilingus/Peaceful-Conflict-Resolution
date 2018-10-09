using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {
    public Transform target;

    private void FixedUpdate()
    {
        transform.position = target.position;
    }

}

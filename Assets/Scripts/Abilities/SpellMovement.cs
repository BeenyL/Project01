using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellMovement : Fireball
{
    Rigidbody rb;
    public float speed = .1f;

    //fireball movement
    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        rb.AddForce(gameObject.transform.forward * speed);
    }
}

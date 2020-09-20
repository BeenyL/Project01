using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bumper : Enemy
{
    [SerializeField] float PushForce = 100f;
    // [SerializeField] Health health;
    //override PlayerImpact adding knockback on top of damaging player.

    protected override void PlayerImpact(PlayerMovement player)
    {   
        Knockback(player);
        Debug.Log("pushed");
    }
    void Knockback(PlayerMovement player)
    {
        Rigidbody rb = player.GetComponent<Rigidbody>();
        Vector3 playerPosition = transform.position - player.transform.position;

        rb.AddForce(-playerPosition * PushForce);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Bumper : Enemy
{
    [SerializeField] float PushForce = 100f;
    [SerializeField] Transform playerTransform;
    // [SerializeField] Health health;
    //override PlayerImpact adding knockback on top of damaging player.

    protected override void PlayerImpact(PlayerMovement player)
    {
        IEnumerator KnockbackTimer()
        {
            PlayerProperty playerproperty = player.GetComponent<PlayerProperty>();
            PlayerMovement playermovement = player.GetComponent<PlayerMovement>();
            playermovement.velocity = playermovement.transform.forward * -(5);
            playermovement.velocity.y = (2);
            playerproperty.isHurt = true;
            Debug.Log(playerproperty.isHurt);
            yield return new WaitForSeconds(.5f);
            playermovement.velocity = Vector3.zero;
            playermovement.velocity.y = 0;
            playerproperty.isHurt = false;
            Debug.Log(playerproperty.isHurt);
        }
        StartCoroutine(KnockbackTimer());
        Debug.Log("pushed");
    }

}

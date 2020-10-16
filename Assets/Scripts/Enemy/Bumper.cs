using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Bumper : Enemy
{
    [SerializeField] float PushForce = 5f;
    [SerializeField] Transform playerTransform;
    [SerializeField] GameObject Main;

    //override PlayerImpact adding knockback on top of damaging player.
    protected override void PlayerImpact(PlayerMovement player)
    {
        IEnumerator KnockbackTimer()
        {
            PlayerProperty playerproperty = player.GetComponent<PlayerProperty>();
            PlayerMovement playermovement = player.GetComponent<PlayerMovement>();
            playermovement.velocity = playermovement.transform.forward * -(PushForce);
            playermovement.velocity.y = (2);
            playerproperty.isHurt = true;
            yield return new WaitForSeconds(.5f);
            playermovement.velocity = Vector3.zero;
            playermovement.velocity.y = 0;
            playerproperty.isHurt = false;
        }
        StartCoroutine(KnockbackTimer());
    }

    protected override void Die()
    {
        if (_CurrentHealth <= 0)
        {
            _CurrentHealth = 0;
            Destroy(Main, .5f);
        }
    }

}

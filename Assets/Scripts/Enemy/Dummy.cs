﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy : Enemy
{
    [SerializeField] float PushForce = 5f;
    [SerializeField] AudioSource soundfx;
    [SerializeField] AudioClip bounced;
    [SerializeField] GameObject Main;

    //dummy modified playerimpact
    protected override void PlayerImpact(PlayerMovement player)
    {
        IEnumerator KnockbackTimer()
        {
            soundfx.volume = .85f;
            soundfx.PlayOneShot(bounced);
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

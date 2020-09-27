using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy : Enemy
{
    [SerializeField] float PushForce = 5f;
    [SerializeField] AudioSource soundfx;
    [SerializeField] AudioClip bounced;

    //dummy modified playerimpact
    protected override void PlayerImpact(PlayerMovement player)
    {
        IEnumerator KnockbackTimer()
        {
            soundfx.PlayOneShot(bounced);
            PlayerProperty playerproperty = player.GetComponent<PlayerProperty>();
            PlayerMovement playermovement = player.GetComponent<PlayerMovement>();
            playermovement.velocity = playermovement.transform.forward * -(PushForce);
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

    protected override void Die()
    {
        base.Die();
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy : Enemy
{
   [SerializeField] ParticleSystem HitParticle;
   [SerializeField] AudioSource Soundfx;
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Projectile")
        {
            Fireball fireball = other.GetComponent<Fireball>();
            PlayerProperty playerproperty = FindObjectOfType<PlayerProperty>();
            if (fireball != null)
            {
                TakeDamage(fireball.Dmg + playerproperty.RageBoost);
                playerproperty.increaseRage(1);
                Soundfx.Play();
                HitParticle.Play();
                Die();
            }
        }
    }

    protected override void Die()
    {
        if(_CurrentHealth <= 0)
        {
            Destroy(gameObject, .5f);
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy : MonoBehaviour
{
   [SerializeField] Health _health;
   [SerializeField] ParticleSystem HitParticle;
   [SerializeField] AudioSource Soundfx;
   [SerializeField] PlayerProperty playerproperty;
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Projectile")
        {
            Fireball fireball = other.GetComponent<Fireball>();
            PlayerProperty playerproperty = FindObjectOfType<PlayerProperty>();
            if (fireball != null)
            {
                _health.TakeDamage(fireball.Dmg + playerproperty.RageBoost);
                playerproperty.increaseRage(1);
                Soundfx.Play();
                HitParticle.Play();
                _health.Die();
            }
        }
    }

}

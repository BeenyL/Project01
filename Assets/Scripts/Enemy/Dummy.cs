using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy : MonoBehaviour
{
   [SerializeField] Health _health;
   [SerializeField] ParticleSystem HitParticle;
   [SerializeField] AudioSource Soundfx;

    private void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Projectile")
        {
            Fireball fireball = other.GetComponent<Fireball>();
            if(fireball != null)
            {
                _health.TakeDamage(fireball.Dmg);
                Soundfx.Play();
                HitParticle.Play();
                _health.Die();
            }

        }
    }

}

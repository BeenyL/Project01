using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : Health
{
    [SerializeField] int _dmg;
    [SerializeField] PlayerProperty playerproperty;
    [SerializeField] PlayerHUD playerhud;
    [SerializeField] Slider enemyhealthSlider;
    [SerializeField] ParticleSystem HitParticle;
    [SerializeField] AudioSource Soundfx;
    [SerializeField] AudioClip[] clips;

    int healthCheck;

    bool isTriggered;
    public int _Dmg { get => _dmg; set => _dmg = value; }

    private void Awake()
    {
        healthCheck = _CurrentHealth;
        enemyhealthSlider.maxValue = _CurrentHealth;
        enemyhealthSlider.value = _CurrentHealth;
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerMovement player = other.gameObject.GetComponent<PlayerMovement>();
        if (player != null)
        {
            PlayerImpact(player);
            playerproperty.TakeDamage(_dmg);
            playerhud.updateHealthSlider();
        }

            if (other.gameObject.tag == "Projectile")
            {
            Soundfx.clip = clips[0];

                Fireball fireball = other.GetComponent<Fireball>();
                PlayerProperty playerproperty = FindObjectOfType<PlayerProperty>();
                if (fireball != null)
                {
                int rageAmt = 1;
                    if(playerproperty.isRage == true)
                {
                    rageAmt = 0;
                }
                else
                {
                    rageAmt = 1;
                }

                    TakeDamage(fireball.Dmg + playerproperty.RageBoost);
                    playerproperty.increaseRage(rageAmt);
                    Soundfx.Play();
                    HitParticle.Play();
                    checkhealth();
                    Die();
                }
            }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Aoe")
        {
            Soundfx.clip = clips[1];

            if (isTriggered == false)
            {
                StartCoroutine(damageOverTime());
                isTriggered = true;
            }
            IEnumerator damageOverTime()
            {
                TakeDamage(1);
                Soundfx.Play();
                HitParticle.Play();
                yield return new WaitForSeconds(.25f);
                isTriggered = false;
            }
            checkhealth();
            Die();
        }
    }

    void checkhealth()
    {
        if(_CurrentHealth < healthCheck)
        {
            healthCheck = _CurrentHealth;
            enemyhealthSlider.value = healthCheck;
        }
    }

    protected virtual void PlayerImpact(PlayerMovement player)
    {

    }

    protected override void Die()
    {

    }

}

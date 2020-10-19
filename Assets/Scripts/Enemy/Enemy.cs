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
    [SerializeField] Transform target;
    [SerializeField] Rigidbody rb;
    int healthCheck;

    bool isTriggered;


    bool Attacked = false;
    public bool IAttacked { get => Attacked; set => Attacked = value; }

    public int _Dmg { get => _dmg; set => _dmg = value; }

    private void Awake()
    {
        healthCheck = _CurrentHealth;
        enemyhealthSlider.maxValue = _CurrentHealth;
        enemyhealthSlider.value = _CurrentHealth;
    }

    private void Update()
    {
        enemyhealthSlider.transform.LookAt(target);
    }

    //hit detections
    private void OnTriggerEnter(Collider other)
    {
        PlayerMovement player = other.gameObject.GetComponent<PlayerMovement>();
        rb.GetComponent<Rigidbody>();
        if (player != null)
        {
            PlayerImpact(player);
            playerproperty.TakeDamage(_dmg);
            playerhud.updateHealthSlider();
        }

            if (other.gameObject.tag == "Projectile" && gameObject.tag == "Enemy")
            {
            Soundfx.clip = clips[0];

                Fireball fireball = other.GetComponent<Fireball>();
                PlayerProperty playerproperty = FindObjectOfType<PlayerProperty>();
                if (fireball != null)
                {
                float rageAmt = 1f;
                    if(playerproperty.isRage == true)
                {
                    rageAmt = 0f;
                }
                else
                {
                    rageAmt = 1f;
                }
                    rb.AddForce(transform.forward * 150f);
                    TakeDamage(fireball.Dmg + playerproperty.RageBoost);
                    playerproperty.increaseRage(rageAmt);
                    Soundfx.Play();
                    HitParticle.Play();
                    checkhealth();
                    Die();
                }
            }
    }

    //player ultimate hit detection
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
                rb.AddForce(transform.forward * 100f);
                TakeDamage(4);
                Soundfx.Play();
                HitParticle.Play();
                yield return new WaitForSeconds(.25f);
                isTriggered = false;
            }
            checkhealth();
            Die();
        }
    }

    //update enemy healthbar
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
        if (_CurrentHealth <= 0)
        {
            _CurrentHealth = 0;
            Destroy(gameObject, .5f);
        }
    }

}

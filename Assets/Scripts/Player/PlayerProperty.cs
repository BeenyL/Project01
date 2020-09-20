using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PlayerProperty : Health
{
    [SerializeField] PlayerHUD playerhud;
    [SerializeField] ParticleSystem rageEffect;
    [SerializeField] PlayerMovement playermovement;
    Fireball fireball;

    [SerializeField] int Rage;
    int _maxRage;
    int _currentRage;
    int _maxHealth;
    int _rageBoost;

    bool _isDead = false;

    public event Action StartBuff = delegate { };
    public event Action StartDeath = delegate { };
    public bool isDead { get => _isDead; set => _isDead = value; }
    public int RageBoost { get => _rageBoost; set => _rageBoost = value; }
    public int CurrentRage{ get => _currentRage; set => _currentRage = value; }
    public int MaxRage { get => _maxRage; set => _maxRage = value; }
    public int MaxHealth { get => _maxHealth; set => _maxHealth = value;}
    private void Awake()
    {
        _maxRage = Rage;
        _currentRage = 0;
        _maxHealth = _CurrentHealth;
        playerhud.HealthBarAmt(_maxHealth);
        playerhud.RageBarAmt(_maxRage);
    }
    private void Start()
    {
        playerhud.updateHealthSlider();
        playerhud.updateRageSlider();
    }
    //future implementation
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("bump");
        TakeDamage(5);
        playerhud.updateHealthSlider();
    }
    private void OnTriggerEnter(Collider other)
    {

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            _currentRage = 0;
            playerhud.updateRageSlider();
            TakeDamage(50);
            playerhud.updateHealthSlider();
            Die();
        }
        if(_currentRage != 0)
        {
            playerhud.updateRageSlider();
        }
        rageTrigger();
    }

    //die
    public override void Die()
    {
        if(_CurrentHealth <= 0)
        {
            _isDead = true;
            StartDeath?.Invoke();
        }
    }

    //Rage
    public void increaseRage(int amount)
    {
        _currentRage += amount;
    }
    void rageTrigger()
    {
        bool isFull = false;
        if (_currentRage >= _maxRage)
        {
            _currentRage = _maxRage;
            isFull = true;
        }

        if (Input.GetKeyDown(KeyCode.R) && isFull == true && playermovement.Grounded == true && playermovement.Moving == false)
        {
            StartCoroutine(RageTimer());
            _currentRage = 0;
            playerhud.updateRageSlider();
            isFull = false;
            StartBuff?.Invoke();
        }
        
        IEnumerator RageTimer()
        {
            fireball = GetComponentInChildren<Fireball>();
            rageEffect.Play();
            _rageBoost = 5;
            yield return new WaitForSeconds(10);
            rageEffect.Stop();
            _rageBoost = 0;
        }
    }
}

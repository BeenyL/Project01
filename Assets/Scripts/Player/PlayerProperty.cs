using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PlayerProperty : Health
{
    [SerializeField] PlayerHUD playerhud;
    [SerializeField] GameObject DeathScreen;
    [SerializeField] ParticleSystem rageEffect;
    [SerializeField] PlayerMovement playermovement;
    [SerializeField] AbilityLoadout abilityloadout;
    [SerializeField] AudioSource audio;
    [SerializeField] AudioSource rageAudio;
    [SerializeField] AudioClip soundHit;
    [SerializeField] AudioClip soundDeath;
    Fireball fireball;

    [SerializeField] ParticleSystem Hitparticle;
    [SerializeField] ParticleSystem Dieparticle;

    [SerializeField] int Rage;
    float _maxRage;
    float _currentRage;
    int _maxHealth;
    int _rageBoost;
    int _healthcheck;
    int _currentPoint;
    public int _maxPoint;

    bool _isDead = false;
    bool _ishurt = false;
    bool _israge = false;

    public event Action StartBuff = delegate { };
    public event Action StartHurt = delegate { };
    public event Action StartDeath = delegate { };
    public bool isDead { get => _isDead; set => _isDead = value; }
    public bool isHurt { get => _ishurt; set => _ishurt = value; }
    public bool isRage { get => _israge; set => _israge = value; }
    public int RageBoost { get => _rageBoost; set => _rageBoost = value; }
    public float CurrentRage{ get => _currentRage; set => _currentRage = value; }
    public float MaxRage { get => _maxRage; set => _maxRage = value; }
    public int MaxHealth { get => _maxHealth; set => _maxHealth = value;}
    public int CurrentPoint { get => _currentPoint; set => _currentPoint = value;}
    public int MaxPoint { get => _maxPoint; set => _maxPoint = value;}
    private void Awake()
    {

        _healthcheck = _CurrentHealth;
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
        playerhud.updatePoint();
    }

    private void Update()
    {
        if(_currentRage != 0)
        {
         playerhud.updateRageSlider();
        }
        rageTrigger();
        RageCountdown(_israge);
        CheckHealth();
        Die();
    }

    //point system

    public void addPoint(int value)
    {
        _currentPoint += value;
    }

    //player damaged/healed checker
    void CheckHealth()
    {
        if (_CurrentHealth < _healthcheck && _CurrentHealth != 0 && _isDead == false)
        {
            audio.volume = .65f;
            audio.PlayOneShot(soundHit);
            Hitparticle.Play();
            StartHurt?.Invoke();
            _healthcheck = _CurrentHealth;
        }
        if (_CurrentHealth > _healthcheck && _isDead == false)
        {
            _healthcheck = _CurrentHealth;
        }
    }

    //die
    protected override void Die()
    {
        if (_CurrentHealth <= 0 && _isDead == false)
        {
            DeathScreen.SetActive(true);
            _CurrentHealth = 0;
            StartCoroutine(DieSequence());
        }
    }
    IEnumerator DieSequence()
    {
        audio.volume = .65f;
        audio.PlayOneShot(soundDeath);
        _isDead = true;
        StartDeath?.Invoke();
        yield return new WaitForSeconds(1.25f);
        Dieparticle.Play();
    }

    //Rage
    public void increaseRage(float amount)
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

        if (Input.GetKeyDown(KeyCode.R) && isFull == true && playermovement.Grounded == true && playermovement.Moving == false && _isDead == false)
        {
            StartCoroutine(RageTimer());
            StartBuff?.Invoke();
            abilityloadout.UseUltimateAbility();
            isFull = false;
        }

        IEnumerator RageTimer()
        {
            fireball = GetComponentInChildren<Fireball>();
            audio.volume = .65f;
            _israge = true;
            rageAudio.Play();
            rageEffect.Play();
            _rageBoost = 5;
            yield return new WaitForSeconds(10);
            _israge = false;
            rageAudio.Stop();
            rageEffect.Stop();
            _rageBoost = 0;
            _currentRage = 0;
        }
    }
    //when player in rage start decreasing rage
    void RageCountdown(bool isEnraged)
    {
        if (isEnraged == true)
        {
                _currentRage -= (1*Time.deltaTime);
                playerhud.updateRageSlider();
        }
    }
}

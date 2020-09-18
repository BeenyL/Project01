using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] Health _health;
    [SerializeField] PlayerHUD playerhud;
    int _maxHealth;
    public int MaxHealth { get => _maxHealth; set => _maxHealth = value;}
    private void Awake()
    {
        _maxHealth = _health._CurrentHealth;
    }
    //future implementation
    private void OnTriggerEnter(Collider other)
    {
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            _health.TakeDamage(5);
            playerhud.updateHealthSlider();
            _health.Die();
        }
    }
}

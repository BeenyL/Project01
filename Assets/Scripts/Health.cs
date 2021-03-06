﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int _currentHealth = 50;

    public int _CurrentHealth { get => _currentHealth; set => _currentHealth = value; }

    public void TakeDamage(int amount)
    {
        _currentHealth -= amount;
        Debug.Log(_currentHealth);
    }

    public void Heal(int amount)
    {
        _currentHealth += amount;
    }

    protected virtual void Die()
    {

    }

}

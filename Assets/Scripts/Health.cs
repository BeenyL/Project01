using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int _currentHealth = 50;

    public void Heal(int amount)
    {
        _currentHealth += amount;
        Debug.Log(gameObject.name + " current health: " + _currentHealth);
    }

    public void TakeDamage(int amount)
    {
        _currentHealth -= amount;
        Debug.Log(_currentHealth);
    }

    public void Die()
    {
        if(_currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

}

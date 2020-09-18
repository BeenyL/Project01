using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    [SerializeField] Health health;
    [SerializeField] Slider healthBar;
    [SerializeField] Text healthPoint;
    [SerializeField] PlayerHealth playerhealth;
    public void updateHealthSlider()
    {
        healthBar.value = health._CurrentHealth;
        healthPoint.text = health._CurrentHealth.ToString() + " / " + playerhealth.MaxHealth.ToString();
    }
}

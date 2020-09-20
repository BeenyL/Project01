using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    [SerializeField] Health health;
    [SerializeField] Slider healthBar;
    [SerializeField] Slider rageBar;

    [SerializeField] Text healthPoint;
    [SerializeField] Text ragePoint;
    [SerializeField] PlayerProperty player;
    public void updateHealthSlider()
    {
        healthBar.value = health._CurrentHealth;
        healthPoint.text = health._CurrentHealth.ToString() + " / " + player.MaxHealth.ToString();
    }
    public void updateRageSlider()
    {
        rageBar.value = player.CurrentRage;
        ragePoint.text = player.CurrentRage.ToString() + " / " + player.MaxRage.ToString();
    }
    public void HealthBarAmt(int HealthAmt)
    {
        healthBar.maxValue = HealthAmt;
    }

    public void RageBarAmt(int RageAmt)
    {
        rageBar.maxValue = RageAmt;
    }

}

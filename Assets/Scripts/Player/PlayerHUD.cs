using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    [SerializeField] Health Playerhealth;
    [SerializeField] Slider healthBar;
    [SerializeField] Slider rageBar;

    [SerializeField] Text healthPoint;
    [SerializeField] Text ragePoint;
    [SerializeField] Text gemPoint;
    [SerializeField] PlayerProperty player;

    [SerializeField] Image healthBarImg;
    [SerializeField] Image rageBarImg;

    [SerializeField] Color defaultRageColor;
    [SerializeField] Color defaultHealthColor;
    Color MaxRageColor = Color.red;
    Color LowHealth = Color.red;

    //update hud values
    public void updateHealthSlider()
    {
        healthBar.value = Playerhealth._CurrentHealth;
        if (Playerhealth._CurrentHealth <= 10)
        {
            healthBarImg.color = LowHealth;
        }
        else
        {
            healthBarImg.color = defaultHealthColor;
        }
        healthPoint.text = Playerhealth._CurrentHealth.ToString() + " / " + player.MaxHealth.ToString();
    }
    public void updateRageSlider()
    {
        rageBar.value = player.CurrentRage;
        if(player.CurrentRage >= 10)
        {
            rageBarImg.color = MaxRageColor;
        }
        else if(player.CurrentRage < 10)
        {
            rageBarImg.color = defaultRageColor;
        }
        ragePoint.text = player.CurrentRage.ToString("F0") + " / " + player.MaxRage.ToString("F0");
    }

    public void updatePoint()
    {
        gemPoint.text = "Points: " + player.CurrentPoint.ToString() + " / " + player.MaxPoint.ToString();
    }

    public void HealthBarAmt(int HealthAmt)
    {
        healthBar.maxValue = HealthAmt;
    }

    public void RageBarAmt(float RageAmt)
    {
        rageBar.maxValue = RageAmt;
    }

}

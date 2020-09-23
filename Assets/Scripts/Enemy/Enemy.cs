using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : Health
{
    //Health health;
    [SerializeField] int _dmg;
    [SerializeField] PlayerProperty playerproperty;
    [SerializeField] PlayerHUD playerhud;
    public int _Dmg { get => _dmg; set => _dmg = value; }

    private void OnTriggerEnter(Collider other)
    {
        PlayerMovement player = other.gameObject.GetComponent<PlayerMovement>();
        if (player != null)
        {
            PlayerImpact(player);
            playerproperty.TakeDamage(_dmg);
            playerhud.updateHealthSlider();
        }
    }

    protected virtual void PlayerImpact(PlayerMovement player)
    {

    }

    protected override void Die()
    {

    }

}

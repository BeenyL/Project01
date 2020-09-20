using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{
    Health health;
    Rigidbody rb;
    [SerializeField] int _dmg;
    public int _Dmg { get => _dmg; set => _dmg = value; }

    private void OnCollisionEnter(Collision other)
    {
        PlayerMovement player = other.gameObject.GetComponent<PlayerMovement>();
        if (player != null)
        {
            PlayerImpact(player);
        }
    }

    protected virtual void PlayerImpact(PlayerMovement player)
    {
       // player.TakeDamage(_dmg);
    }

}

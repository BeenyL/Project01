using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cure : Ability
{
    int _healAmount = 25;

    public override void Use(Transform orgin, Transform target)
    {
        if(target == null) { return;  }
        Debug.Log("cast Cure");
        target.GetComponent<Health>()?.Heal(_healAmount);
    }
}

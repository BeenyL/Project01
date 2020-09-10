using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : Ability
{
    int _healAmount = 3;

    public override void Use(Transform orgin, Transform target)
    {
        if(target == null) { return;  }
        Debug.Log("cast Heal");
        target.GetComponent<Health>()?.Heal(_healAmount);

    }
}

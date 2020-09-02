using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : Ability
{
    [SerializeField] GameObject _projectileSpawned = null;
    int _rank = 1;

    public override void Use(Transform orgin, Transform target)
    {
        GameObject projectile = Instantiate(_projectileSpawned, orgin.position, orgin.rotation);
        if (target == null)
        {
            projectile.transform.LookAt(target);
        }
        Destroy(projectile, 3.5f);

    }

}

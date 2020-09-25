using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameBurst : Ability
{
    [SerializeField] GameObject Spawnedobject = null;
    [SerializeField] int _dmg = 3;
    [SerializeField] Transform SpawnPosition;
    [SerializeField] AudioSource Soundfx;
    public override void Use(Transform orgin)
    {
        Soundfx.Play();
        GameObject Spawn_object = Instantiate(Spawnedobject, SpawnPosition.position, SpawnPosition.rotation);
        Destroy(Spawn_object, 5f);

    }
}

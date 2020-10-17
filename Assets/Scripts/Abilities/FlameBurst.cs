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
        StartCoroutine(StartWave());

    }
    IEnumerator StartWave()
    {
        Spawnedobject.SetActive(true);
        yield return new WaitForSeconds(5f);
        Spawnedobject.SetActive(false);
    }
}

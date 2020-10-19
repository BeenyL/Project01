using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : Ability
{
    [SerializeField] GameObject _projectileSpawned = null;

    [SerializeField] int _amt = 3;
    [SerializeField] int _dmg = 3;
    [SerializeField] Transform[] SpawnPosition;
    [SerializeField] AudioSource Soundfx;
    [SerializeField] AudioClip fireball;
    public int Dmg {get => _dmg; set => _dmg = value;}

    private void Start()
    {
        //find the 3 fireball positions
        for(int i = 0; i < _amt; i++)
        {
            SpawnPosition[i] = GameObject.Find("FireBallSpawnPt" + (i+1).ToString()).transform;
        }
    }
    public override void Use(Transform orgin)
    {
        for (int i = 0; i < _amt; i++)
        {
            Soundfx.volume = .3f;
            Soundfx.Play();
            GameObject projectile = Instantiate(_projectileSpawned, SpawnPosition[i].position, SpawnPosition[i].rotation );
            Destroy(projectile, 1f);
        }
        
    }

}

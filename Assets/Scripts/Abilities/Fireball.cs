using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : Ability
{
    [SerializeField] GameObject _projectileSpawned = null;
    //rank not developed yet
    int _rank = 1;
    [SerializeField] int _amt = 3;
    [SerializeField] int _dmg = 3;
    [SerializeField] Transform[] SpawnPosition;
    [SerializeField] AudioSource Soundfx;
    [SerializeField] Rigidbody rb_Fireball;
    public int Dmg {get => _dmg; set => _dmg = value;}

    private void Start()
    {
        //find the 3 fireball positions
        for(int i = 0; i < _amt; i++)
        {
            SpawnPosition[i] = GameObject.Find("FireBallSpawnPt" + (i+1).ToString()).transform;
        }
    }
    public override void Use(Transform orgin, Transform target)
    {
        
        for(int i = 0; i < _amt; i++)
        {
            //Debug.Log(SpawnPosition.Length);
            Soundfx.Play();
            GameObject projectile = Instantiate(_projectileSpawned, SpawnPosition[i].position, SpawnPosition[i].rotation );
            if (target == null)
            {
                projectile.transform.LookAt(target);
            }
            
            Destroy(projectile, 1f);
        }
        
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateLightTower : MonoBehaviour
{
    float speed = 45;
    void Update()
    {
        gameObject.transform.Rotate(0, Time.deltaTime * speed, 0);
    }
}

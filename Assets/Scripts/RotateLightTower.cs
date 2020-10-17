using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateLightTower : MonoBehaviour
{
    float speed = 45;
    float hSpeed = 5f;
    float height = .5f;

    Vector3 pos;

    private void Start()
    {
        pos = transform.position;
    }

    void Update()
    {
        gameObject.transform.Rotate(0, Time.deltaTime * speed, 0);
        float newY = Mathf.Sin(Time.time * hSpeed) * height + pos.y;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}

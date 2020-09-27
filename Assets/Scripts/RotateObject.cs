using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    float speed = 45;
    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Rotate(Time.deltaTime * speed, Time.deltaTime * speed, Time.deltaTime * speed);
    }
}

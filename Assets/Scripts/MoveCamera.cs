using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UIElements;
using System.Numerics;

public class MoveCamera : MonoBehaviour
{
    [SerializeField] CinemachineFreeLook freelookcam;

    private void Update()
    {
        float _posX;
        float _posY;
       // Vector2 Pos = new Vector2 (Input.mousePosition.x,Input.mousePosition.y);
        if (Input.GetMouseButton(1))
        {
            freelookcam.m_XAxis.Value = (Input.mousePosition.x);
            freelookcam.m_YAxis.Value = (Input.mousePosition.y);
        }
        if (Input.GetMouseButtonUp(1))
        {

        }

    }




}

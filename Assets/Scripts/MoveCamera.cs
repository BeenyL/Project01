using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UIElements;

public class MoveCamera : MonoBehaviour
{
    [SerializeField] CinemachineFreeLook freelookcam;

    private void Update()
    {
        if(Input.GetMouseButton(1))
        {
            freelookcam.m_XAxis.Value = Input.mousePosition.x;
            freelookcam.m_YAxis.Value = Input.mousePosition.y;
        }



    }




}

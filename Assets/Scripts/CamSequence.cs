using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamSequence : MonoBehaviour
{
   [SerializeField] GameObject mainCam;
   [SerializeField] GameObject cutSceneCam;
    void Start()
    {
        StartCoroutine(SwitchCamera());
    }

    IEnumerator SwitchCamera()
    {
        yield return new WaitForSeconds(4f);
        mainCam.SetActive(true);
        cutSceneCam.SetActive(false);
    }

}

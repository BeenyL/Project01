using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemCube : MonoBehaviour
{
    [SerializeField] PlayerProperty playerproperty;
    [SerializeField] PlayerHUD playerhud;
    [SerializeField] AudioSource audio;
    [SerializeField] MeshRenderer meshrender;
    [SerializeField] MeshCollider meshcollider;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerproperty = other.gameObject.GetComponent<PlayerProperty>();
            if (playerproperty != null)
            {
                audio.Play();
                playerproperty.addPoint(1);
                playerhud.updatePoint();
                meshrender.enabled = false;
                meshcollider.enabled = false;
            }
            else
            {
                return;
            }
        }

    }

}

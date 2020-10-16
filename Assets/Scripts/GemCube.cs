using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemCube : MonoBehaviour
{
    [SerializeField] PlayerProperty playerproperty;
    [SerializeField] PlayerHUD playerhud;
    [SerializeField] AudioSource audio;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerproperty = other.gameObject.GetComponent<PlayerProperty>();
            if (playerproperty != null)
            {
                audio.Play();
                playerproperty.addPoint(1);
                playerhud.updatePoint();
                Destroy(gameObject);
            }
            else
            {
                return;
            }
        }

    }

}

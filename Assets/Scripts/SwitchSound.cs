using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchSound : MonoBehaviour
{
    [SerializeField] AudioSource playerAudio;
    [SerializeField] AudioClip[] clips;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
        playerAudio.clip = clips[0];
        playerAudio.volume = .5f;
        playerAudio.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
        playerAudio.clip = clips[1];
        playerAudio.volume = .15f;
        playerAudio.Play();
        }
    }
}

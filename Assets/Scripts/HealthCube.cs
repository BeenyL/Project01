using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCube : MonoBehaviour
{
    [SerializeField] PlayerProperty playerproperty;
    [SerializeField] PlayerHUD playerhud;
    [SerializeField] AudioSource audio;
    [SerializeField] MeshRenderer meshrender;
    [SerializeField] BoxCollider meshcollider;
    [SerializeField] ParticleSystem particle;
    [SerializeField] Light light;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerproperty = other.gameObject.GetComponent<PlayerProperty>();
            if (playerproperty != null && playerproperty._CurrentHealth < playerproperty.MaxHealth)
            {
                audio.Play();
                playerproperty.Heal(5);
                playerhud.updateHealthSlider();
                meshrender.enabled = false;
                meshcollider.enabled = false;
                particle.Stop();
                light.enabled = false;
                Debug.Log(playerproperty._CurrentHealth);
            }
            else
            {
                return;
            }
        }

    }

}

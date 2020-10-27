using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : MonoBehaviour
{
    [SerializeField] AudioClip healthSFX = null;
    [SerializeField] float _powerupDuration = 5;

    AudioSource audioSource = null;

    [Header("Setup")]
    //GameObject _visualsToDeactivate;

    Collider _colliderToDeactivate = null;

    private void Awake()
    {
        _colliderToDeactivate = GetComponent<Collider>();

        audioSource = GetComponent<AudioSource>();

        EnableObject();
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerController playerController
            = other.gameObject.GetComponent<PlayerController>();
        if (other.tag == "Player")
        {
            // start powerup timer. restart, if it's already started
            StartCoroutine(PowerupSequence(playerController));
            if (audioSource != null && healthSFX != null)
            {
                audioSource.clip = healthSFX;
                audioSource.Play();
            }

        }
    }

    IEnumerator PowerupSequence(PlayerController playerController)
    {
        DisableObject();
        //wait for the required duration
        yield return new WaitForSeconds(_powerupDuration);
        // reset
        EnableObject();
    }

    public void DisableObject()
    {
        //disable collider, so it can't be retriggered
        _colliderToDeactivate.enabled = false;
        //disable visuals, to simulate deactivated
        //_visualsToDeactivate.SetActive(false);
        //TODO reactivate particle flash/audio

    }

    public void EnableObject()
    {
        //enable collider, so it can be retriggered 
        _colliderToDeactivate.enabled = true;
        //enable visuals again, to draw player attention
        //_visualsToDeactivate.SetActive(true);
        //TODO reactivate particle flash/audio

    }
}

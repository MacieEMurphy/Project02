using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAi : MonoBehaviour
{
    int enemyHealth = 5;
    public Level01Controller level01Controller;
    AudioSource audioSource = null;
    [SerializeField] AudioClip damageSFX = null;
    [SerializeField] AudioClip deathSFX = null;
    

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void TakeDamage(int _damageToTake)
    {
        enemyHealth -= _damageToTake;
        Debug.Log(enemyHealth + " health remaining");
        audioSource.clip = damageSFX;
        audioSource.Play();

        if (enemyHealth <= 0)
        {
            level01Controller.IncreaseScore(5);
            audioSource.clip = deathSFX;
            audioSource.Play();
            gameObject.SetActive(false);

        }
    }

    public void FreezeEnemy()
    {
        GetComponent<TrackingSystem>().enabled = false;
        Debug.Log("Freeze!");
    }
}

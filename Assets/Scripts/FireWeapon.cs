using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWeapon : MonoBehaviour
{
    [SerializeField] Camera cameraController;
    [SerializeField] Transform rayOrigin;
    [SerializeField] float shootDistance = 10f;
    [SerializeField] float punchDistance = 2f;
    [SerializeField] int weaponDamage = 1;
    [SerializeField] AudioClip shotSFX = null;
    [SerializeField] AudioClip punchSFX = null;

    AudioSource audioSource = null;
    RaycastHit objectHit;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Punch();
        }
    }

    void Shoot()
    {
        audioSource.clip = shotSFX;
        audioSource.Play();
        Vector3 rayDirection = cameraController.transform.forward;
        Debug.DrawRay(rayOrigin.position, rayDirection * shootDistance, Color.yellow, 1f);
        if(Physics.Raycast(rayOrigin.position, rayDirection, out objectHit, shootDistance))
        {
            Debug.Log("You HIT the " + objectHit.transform.name);
            //enemy detection
            EnemyAi enemyAi = objectHit.transform.gameObject.GetComponent<EnemyAi>();
            if(enemyAi != null)
            {
                enemyAi.TakeDamage(weaponDamage);
                enemyAi.FreezeEnemy();
            }
        }
        else
        {
            Debug.Log("Miss.");
        }
    }

    void Punch()
    {
        audioSource.clip = punchSFX;
        audioSource.Play();
        Vector3 rayDirection = cameraController.transform.forward;
        Debug.DrawRay(rayOrigin.position, rayDirection * punchDistance, Color.blue, 1f);
        if (Physics.Raycast(rayOrigin.position, rayDirection, out objectHit, punchDistance))
        {
            Debug.Log("You HIT the " + objectHit.transform.name);
            //enemy detection
            EnemyAi enemyAi = objectHit.transform.gameObject.GetComponent<EnemyAi>();
            if (enemyAi != null)
            {
                enemyAi.TakeDamage(1);
                Debug.Log("But why though?");
            }
        }
        else
        {
            Debug.Log("Miss.");
        }
    }
}

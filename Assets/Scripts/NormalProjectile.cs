using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalProjectile : BaseProjectile 
{
    Vector3 m_direction;
    bool m_fired;
    public GameObject m_launcher;
    public GameObject m_target;
    int m_damage;
    AudioSource audioSource = null;
    [SerializeField] AudioClip fireSFX = null;
    

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_fired)
        {
            transform.position += m_direction * (speed * Time.deltaTime);

        }
    }

    public override void FireProjectile(GameObject launcher, GameObject target, int damage, float attackSpeed)
    {
        if (launcher && target)
        {
            audioSource.clip = fireSFX;
            audioSource.Play();
            m_direction = (target.transform.position - launcher.transform.position).normalized;
            m_fired = true;
            m_launcher = launcher;
            m_target = target;
            m_damage = damage;

            Destroy(gameObject, 10.0f);
        }
    }

    void OnTriggerEnter(Collider other)
    {
       /* if (other.gameObject == m_target)
        {
            Debug.Log("Player takes damage!");
        } */
        if(other.tag == "Ground")
        {
            Destroy(gameObject);
            Debug.Log("It hit a wall!");
        } 

        //if (other.gameObject.GetComponent<BaseProjectile>() == null)
        //{
            //Destroy(gameObject);
        //}
            
    }

}

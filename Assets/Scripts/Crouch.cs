using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crouch : MonoBehaviour
{
    //private CharacterController m_CharacterController;
    private CapsuleCollider m_Collider;

    private bool m_Crouch = false;
    private float m_originalHeight;
    private float m_crouchHeight = 1.0f;
    public KeyCode crouchKey = KeyCode.C;

    void Start()
    {
        //m_CharacterController = GetComponent<CharacterController>();
        m_Collider = GetComponent<CapsuleCollider>();
        m_originalHeight = m_Collider.height;

    }

    void Update()
    {
        if (Input.GetKeyDown(crouchKey))
        {
            m_Crouch = !m_Crouch;

            CheckCrouch();
        }
    }


        void CheckCrouch()
        {
            if (m_Crouch == true)
            {
            m_Collider.height = m_crouchHeight;
                //m_CharacterController.height = m_crouchHeight;
        }
            else
            {
            m_Collider.height = m_originalHeight;
            //m_CharacterController.height = m_originalHeight;
        }
        }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(FPSInput))]
[RequireComponent(typeof(FPSMotor))]
public class PlayerController : MonoBehaviour
{
    GameObject[] finishObjects;

    FPSInput _input = null;
    FPSMotor _motor = null;
    AudioSource audioSource = null;
    
    public float playerHealth = 5.0f;
    public Slider healthBar;
    public bool alive;

    [SerializeField] float _moveSpeed = 0.1f;
    [SerializeField] float _walkSpeed = 0.1f;
    [SerializeField] float _turnSpeed = 6.0f;
    [SerializeField] float _jumpStrength = 10.0f;
    [SerializeField] AudioClip shotSFX = null;
    [SerializeField] ParticleSystem _muzzleFlash = null;

    private void Start()
    {
        Time.timeScale = 1;

        finishObjects = GameObject.FindGameObjectsWithTag("ShowOnFinish");
        Cursor.lockState = CursorLockMode.Locked;
        hideFinished();
        alive = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            playerHealth -= 1;
            Debug.Log("Player Health: " + playerHealth);
        }

         if(playerHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        gameObject.GetComponent<PlayerController>().alive = false;
        Debug.Log("Player has been killed!");
        Time.timeScale = 0;

    }

    //shows objects with ShowOnFinish tag
    public void showFinished()
    {
        foreach (GameObject g in finishObjects)
        {
            g.SetActive(true);
        }
    }

    //hides objects with ShowOnFinish tag
    public void hideFinished()
    {
        foreach (GameObject g in finishObjects)
        {
            g.SetActive(false);
        }
    }

    private void Awake()
    {
        _input = GetComponent<FPSInput>();
        _motor = GetComponent<FPSMotor>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        _input.MoveInput += OnMove;
        _input.RotateInput += OnRotate;
        _input.JumpInput += OnJump;
    }

    private void OnDisable()
    {
        _input.MoveInput -= OnMove;
        _input.RotateInput -= OnRotate;
        _input.JumpInput -= OnJump;
    }

    void OnMove(Vector3 movement)
    {
        _motor.Move(movement * _moveSpeed);
    }

    void OnRotate(Vector3 rotation)
    {
        _motor.Turn(rotation.y * _turnSpeed);
        _motor.Look(rotation.x * _turnSpeed);
    }

    void OnJump()
    {
        _motor.Jump(_jumpStrength);
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            audioSource.clip = shotSFX;
            audioSource.Play();
            _muzzleFlash.Play();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            _moveSpeed *= 5.0f;
            Debug.Log("Player is sprinting.");
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            _moveSpeed = _walkSpeed;
        }

        healthBar.value = playerHealth;
        //if (Input.GetMouseButtonDown(0))
        if (Time.timeScale == 0 && alive == false)
        {
            showFinished();
        }
    }
}

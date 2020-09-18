﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ThirdPersonMovement : MonoBehaviour
{
    public event Action Idle = delegate { };
    public event Action StartRunning = delegate { };
    public event Action StartJumping = delegate { };
    public event Action StartFalling = delegate { };
    public event Action StartSprinting = delegate { };
    public event Action StartLanding = delegate { };
    public event Action StartChannel = delegate { };

    [SerializeField] CharacterController controller;
    [SerializeField] Camera camera;
    [SerializeField] Transform cam;

    [SerializeField] Transform _groundCheck;
    [SerializeField] float _groundDistance = .04f;
    public LayerMask groundMask;
    bool isGrounded;
   
    public bool Grounded { get => isGrounded; set => isGrounded = value;}
    public bool Moving { get => _isMoving; set => _isMoving = value; }

    public float _speed;
    [SerializeField] float _BasedSpeed = 6f;
    [SerializeField] float _SprintSpeed = 12f;
    [SerializeField] float _jumpHeight = 3f;
    [SerializeField] float _gravity = -9.81f;
    [SerializeField] float _turnSmoothTime = .01f;
    public Vector3 velocity;
    float _turnSmoothVelocity;

    bool _isMoving = false;
    bool _isFalling = false;
    bool _landed = true;
    bool _jumped = false;
    bool _isSprinting = false;
    bool _isChannel = false;
    private void Start()
    {
        Idle?.Invoke();
    }
    private void Update()
    {
        Movement();
        Spells();
    }

    private void Movement()
    {
        //movement section
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        //sprint detection
        if (!Input.GetKey(KeyCode.LeftShift))
        {
            _isSprinting = false;
            _speed = _BasedSpeed;
        }
        else
        {
            _isSprinting = true;
            _speed = _SprintSpeed;
        }

        //idle rotation
        if(direction.magnitude == 0 && Input.GetMouseButton(1))
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, _turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 standDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.zero;
            gameObject.transform.Rotate(standDir.normalized);
        }
        //movement
        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, _turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * _speed * Time.deltaTime);

            // walk/sprint state change (grounded to keep sprinting from playing during jump)
            if (Input.GetKeyDown(KeyCode.LeftShift) && isGrounded)
            {
                StartSprinting?.Invoke();
            }
            else if (Input.GetKeyUp(KeyCode.LeftShift) && isGrounded)
            {
                StartRunning?.Invoke();
            }
            else
            {
                CheckIfStartedMoving();
            }
        }
        else
        {
            CheckIfStoppedMoving();
        }

        //jump section
        isGrounded = Physics.CheckSphere(_groundCheck.position, _groundDistance, groundMask);

        velocity.y += _gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        //jump detect
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(_jumpHeight * -2f * _gravity);
            CheckIfStartedJump();
        }

        //jump to fall transition
        if (controller.velocity.y > 2)
        {
            CheckIfStartedFall();
        }

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        //check if player has landed
            if (isGrounded && velocity.y < 0 && _isFalling)
        {
            CheckIfStartedLand();
            CheckIfStoppedMoving();
        }
    }

    //Spells
    private void Spells()
    {
        //channel fireball
        if (Input.GetMouseButtonDown(0))
        {
            CheckIfStartedChannel();
        }
        if (Input.GetMouseButtonUp(0))
        {
            _isChannel = false;
        }
    }
    //decides if player will use sprint or walk animation
    private void CheckIfStartedMoving()
    {
        if (isGrounded == true)
        {
            if (_isMoving == false)
            {
                if(_isSprinting == false)
                {
                    StartRunning?.Invoke();
                }
                else
                {
                    StartSprinting?.Invoke();
                }
            }
            _isMoving = true;
        }
    }

    //check if player stopped moving play idle
    private void CheckIfStoppedMoving()
    {
        if (_isMoving == true && isGrounded == true)
        {
            Idle?.Invoke();
        }
        _isMoving = false; 
    }

    //check and play jump animation
    private void CheckIfStartedJump()
    {
        if(_jumped == false)
        {
            StartJumping?.Invoke();
        }
        _jumped = true;
        _landed = false;
    }

    //check and play falling animation
  private void CheckIfStartedFall()
    {
        if(_isFalling == false)
        {
            StartFalling?.Invoke();
        }
        _isFalling = true;
    }

    //check and play landing animation
    private void CheckIfStartedLand()
    {
        if(_landed == false)
        {
            StartLanding?.Invoke();
        }
        _isFalling = false;
        _jumped = false;
        _landed = true;
    }
    
    //check if player can channel
    private void CheckIfStartedChannel()
    {
        if(_isChannel == false && isGrounded == true && _isMoving == false)
        {
            StartChannel?.Invoke();
        }
        _isChannel = true;
    }
}

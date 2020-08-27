using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ThirdPersonMovement : MonoBehaviour
{
    public event Action Idle = delegate { };
    public event Action StartRunning = delegate { };
    public event Action StartJumping = delegate { };
    public event Action StartFalling = delegate { };

    [SerializeField] CharacterController controller;
    [SerializeField] Transform cam;

    [SerializeField] Transform _groundCheck;
    [SerializeField] float _groundDistance = .04f;
    public LayerMask groundMask;
    bool isGrounded;

    [SerializeField] float _speed = 6f;
    [SerializeField] float _jumpHeight = 3f;
    [SerializeField] float _gravity = -9.18f;
    [SerializeField] float _turnSmoothTime = .01f;
    Vector3 velocity;

    float _turnSmoothVelocity;
    bool _isMoving = false;

    private void Start()
    {
        Idle?.Invoke();
    }
    private void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        float upward = Input.GetAxisRaw("Jump");

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        isGrounded = Physics.CheckSphere(_groundCheck.position, _groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
            CheckIfStoppedFall();
        }

        velocity.y += _gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(_jumpHeight * -2f * _gravity);
            CheckIfStartedJump();
            CheckIfStartedFall();
        }

        if (direction.magnitude >= 0.1f)
        {
            CheckIfStartedMoving();
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, _turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * _speed * Time.deltaTime);
        }
        else
        {
            CheckIfStoppedMoving();
        }
    }

    private void CheckIfStartedMoving()
    {
        if(_isMoving == false)
        {
            StartRunning?.Invoke();
            Debug.Log("Started");
        }
        _isMoving = true;
    }

    private void CheckIfStoppedMoving()
    {
        if(_isMoving == true)
        {
            Idle?.Invoke();
            Debug.Log("Stopped");
        }
        _isMoving = false; 
    }

    private void CheckIfStartedJump()
    {
        if(isGrounded == true)
        {
            StartJumping?.Invoke();

        }
        isGrounded = false;
    }

    //fix checkif started and stopped fall.
    private void CheckIfStartedFall()
    {
        if(isGrounded == false)
        {
            StartFalling?.Invoke();
        }
    }

    private void CheckIfStoppedFall()
    {
        if(isGrounded == true)
        {
            Idle?.Invoke();
        }
    }
}

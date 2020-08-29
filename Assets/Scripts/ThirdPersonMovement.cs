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
    public event Action StartSprinting = delegate { };
    public event Action StartLanding = delegate { };

    [SerializeField] CharacterController controller;
    [SerializeField] Transform cam;

    [SerializeField] Transform _groundCheck;
    [SerializeField] float _groundDistance = .04f;
    public LayerMask groundMask;
    bool isGrounded;

    [SerializeField] float _speed = 6f;
    [SerializeField] float _SprintSpeed = 6f;
    [SerializeField] float _jumpHeight = 3f;
    [SerializeField] float _gravity = -9.81f;
    [SerializeField] float _turnSmoothTime = .01f;
    public Vector3 velocity;
    float _turnSmoothVelocity;

    bool _isMoving = false;
    bool _isFalling = false;
    bool _landed = true;
    bool _landedIdle = false;
    bool _jumped = false;
    bool _Sprinted = false;
    bool _isSprinting = false;
    private void Start()
    {
        Idle?.Invoke();
    }
    private void Update()
    {
        Movement();
    }

    private void Movement()
    {
        //movement
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

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
        //sprint detection
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            CheckIfStartedSprinting();
            Sprint(true);
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            CheckIfStoppedSprinting();
            Sprint(false);
        }

        //jump
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
        if (velocity.y == 0)
        {
            //Debug.Log("0");
            CheckIfStartedFall();
        }

        //check if player has landed
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
            CheckIfStoppedFall();
        }

    }

    private void Sprint(bool _Sprinted)
    {
        if(_Sprinted == true)
        {
            _speed += _SprintSpeed;
        }
        if (_Sprinted == false)
        {
            _speed -= _SprintSpeed;
        }
    }

    private void CheckIfStartedMoving()
    {
        if(_isMoving == false)
        {
            StartRunning?.Invoke();
            Debug.Log("walking");
        }
        _isMoving = true;
    }

    private void CheckIfStoppedMoving()
    {
        if(_isMoving == true)
        {
            Idle?.Invoke();
            Debug.Log("Standing");
        }
        _isMoving = false; 
    }

    private void CheckIfStartedJump()
    {
        if(_jumped == false)
        {
            StartJumping?.Invoke();
            Debug.Log("jumping");
        }
        _jumped = true;
        _landed = false;
    }

  private void CheckIfStartedFall()
    {
        if(_isFalling == false)
        {
            StartFalling?.Invoke();
            Debug.Log("falling");
        }
        _isFalling = true;
    }

    private void CheckIfStoppedFall()
    {
        if(_landed == false)
        {
            StartLanding?.Invoke();
            Debug.Log("landed");
        }
        _isFalling = false;
        _jumped = false;
        _landed = true;
    }
 
    //implement way to get it to idle after jump
    private void CheckIfLanded()
    {
        if (_landedIdle == true)
        {
            Idle?.Invoke();
        }
        _landedIdle = false;
    }

    private void CheckIfStartedSprinting()
    {
        if (_isSprinting == false)
        {
            StartSprinting?.Invoke();
            Debug.Log("running");
        }
        _isSprinting = true;
    }

    private void CheckIfStoppedSprinting()
    {
        if (_isSprinting == true)
        {
            StartRunning?.Invoke();
            Debug.Log("running");
        }
        _isSprinting = false;
    }


}

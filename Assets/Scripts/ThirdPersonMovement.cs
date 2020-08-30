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

        //sprint detection (changes speed)
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

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, _turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * _speed * Time.deltaTime);

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                StartSprinting?.Invoke();
            }

            else if (Input.GetKeyUp(KeyCode.LeftShift))
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
        if (controller.velocity.y > 2)
        {
            CheckIfStartedFall();
        }

        //check if player has landed
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
            CheckIfStoppedFall();
        }

    }

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

    private void CheckIfStoppedMoving()
    {
        if(_isMoving == true)
        {
            Idle?.Invoke();
            //Debug.Log("Standing");
        }
        _isMoving = false; 
    }

    private void CheckIfStartedSprint()
    {
        if (isGrounded == true)
        {
            if (_isSprinting == false)
            {
                Debug.Log("called");
                StartSprinting?.Invoke();
            }
        }
        _isSprinting = true;
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

}

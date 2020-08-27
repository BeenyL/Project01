using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerCharacterAnimator : MonoBehaviour
{
    [SerializeField] ThirdPersonMovement _thirdpersonmovement = null;
    const string IdleState = "Idle";
    const string RunState = "Run";
    const string JumpState = "Jumping";
    const string FallState = "Falling";

    Animator _animator = null;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void OnIdle()
    {
        _animator.CrossFadeInFixedTime(IdleState, .2f);
    }

    public void OnStartRunning()
    {
        _animator.CrossFadeInFixedTime(RunState, .2f);
    }

    public void OnStartJumping()
    {
        _animator.CrossFadeInFixedTime(JumpState, .2f);
    }

    public void OnStartFalling()
    {
        _animator.CrossFadeInFixedTime(FallState, .2f);
    }

    private void OnEnable()
    {
        _thirdpersonmovement.Idle += OnIdle;
        _thirdpersonmovement.StartRunning += OnStartRunning;
        _thirdpersonmovement.StartJumping += OnStartJumping;
        _thirdpersonmovement.StartFalling += OnStartFalling;
    }

    private void OnDisable()
    {
        _thirdpersonmovement.Idle -= OnIdle;
        _thirdpersonmovement.StartRunning -= OnStartRunning;
        _thirdpersonmovement.StartJumping -= OnStartJumping;
        _thirdpersonmovement.StartFalling -= OnStartFalling;
    }

}

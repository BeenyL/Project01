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
    const string LandState = "Landing";
    const string SprintState = "Sprinting";

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

    public void OnStartSprinting()
    {
        _animator.CrossFadeInFixedTime(SprintState, .2f);
    }

    public void OnStartLanding()
    {
        _animator.CrossFadeInFixedTime(LandState, .2f);
    }

    private void OnEnable()
    {
        _thirdpersonmovement.Idle += OnIdle;
        _thirdpersonmovement.StartRunning += OnStartRunning;
        _thirdpersonmovement.StartJumping += OnStartJumping;
        _thirdpersonmovement.StartFalling += OnStartFalling;
        _thirdpersonmovement.StartSprinting += OnStartSprinting;
        _thirdpersonmovement.StartLanding += OnStartLanding;
    }

    private void OnDisable()
    {
        _thirdpersonmovement.Idle -= OnIdle;
        _thirdpersonmovement.StartRunning -= OnStartRunning;
        _thirdpersonmovement.StartJumping -= OnStartJumping;
        _thirdpersonmovement.StartFalling -= OnStartFalling;
        _thirdpersonmovement.StartSprinting -= OnStartSprinting;
        _thirdpersonmovement.StartLanding -= OnStartLanding;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerCharacterAnimator : MonoBehaviour
{
    [SerializeField] PlayerMovement _playermovement = null;
    [SerializeField] PlayerProperty _playerproperty = null;
    const string IdleState = "Idle";
    const string RunState = "Run";
    const string JumpState = "Jumping";
    const string FallState = "Falling";
    const string LandState = "Landing";
    const string SprintState = "Sprinting";
    const string ChannelState = "Channeling";
    const string BuffState = "Buffing";
    const string DieState = "Dying";

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

    public void OnStartChanneling()
    {
        _animator.CrossFadeInFixedTime(ChannelState, .2f);
    }

    public void OnStartBuffing()
    {
        _animator.CrossFadeInFixedTime(BuffState, .2f);
    }

    public void OnStartDie()
    {
        _animator.CrossFadeInFixedTime(DieState, .2f);
    }

    private void OnEnable()
    {
        _playermovement.Idle += OnIdle;
        _playermovement.StartRunning += OnStartRunning;
        _playermovement.StartJumping += OnStartJumping;
        _playermovement.StartFalling += OnStartFalling;
        _playermovement.StartSprinting += OnStartSprinting;
        _playermovement.StartLanding += OnStartLanding;
        _playermovement.StartChannel += OnStartChanneling;
        _playerproperty.StartBuff += OnStartBuffing;
        _playerproperty.StartDeath += OnStartDie;
    }

    private void OnDisable()
    {
        _playermovement.Idle -= OnIdle;
        _playermovement.StartRunning -= OnStartRunning;
        _playermovement.StartJumping -= OnStartJumping;
        _playermovement.StartFalling -= OnStartFalling;
        _playermovement.StartSprinting -= OnStartSprinting;
        _playermovement.StartLanding -= OnStartLanding;
        _playermovement.StartChannel -= OnStartChanneling;
        _playerproperty.StartBuff -= OnStartBuffing;
        _playerproperty.StartDeath -= OnStartDie;
    }

}

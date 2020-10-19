using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerCharacterAnimator : MonoBehaviour
{
    [SerializeField] PlayerMovement _playermovement = null;
    [SerializeField] PlayerProperty _playerproperty = null;
    [SerializeField] AudioSource audio;
    [SerializeField] AudioClip[] clips;
    const string IdleState = "Idle";
    const string RunState = "Run";
    const string JumpState = "Jumping";
    const string FallState = "Falling";
    const string LandState = "Landing";
    const string SprintState = "Sprinting";
    const string ChannelState = "Channeling";
    const string BuffState = "Buffing";
    const string DieState = "Dying";
    const string HurtState = "Damaged";

    Animator _animator = null;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    void soundManagement()
    {
        IEnumerator soundPattern()
        {
            yield return new WaitForSeconds(0.01f);
            audio.Stop();
            while (_playermovement.Moving == true && _playermovement.Grounded == true)
            {
                if (audio.isPlaying == true)
                {
                    audio.Stop();
                }
                audio.Play();
                yield return new WaitForSeconds(.15f);

                if (_playermovement.Grounded == false)
                {
                    audio.Stop();
                }

            }
        }
        StartCoroutine(soundPattern());
    }

    public void OnIdle()
    {
        _animator.CrossFadeInFixedTime(IdleState, .2f);
    }

    public void OnStartRunning()
    {
        audio.volume = .55f;
        audio.clip = clips[0];
        _animator.CrossFadeInFixedTime(RunState, .2f);
        StopAllCoroutines();
        soundManagement();
    }

    public void OnStartJumping()
    {
        audio.volume = .55f;
        StopAllCoroutines();
        audio.PlayOneShot(clips[2]);
        _animator.CrossFadeInFixedTime(JumpState, .2f);
    }

    public void OnStartFalling()
    {
        _animator.CrossFadeInFixedTime(FallState, .2f);
    }

    public void OnStartSprinting()
    {
        audio.volume = .55f;
        StopAllCoroutines();
        audio.clip = clips[1];
        _animator.CrossFadeInFixedTime(SprintState, .2f);
        StopAllCoroutines();
        soundManagement();
    }

    public void OnStartLanding()
    {
        audio.volume = .55f;
        StopAllCoroutines();
        audio.PlayOneShot(clips[3]);
        _animator.CrossFadeInFixedTime(LandState, .2f);
    }

    public void OnStartChanneling()
    {
        _animator.CrossFadeInFixedTime(ChannelState, .2f);
    }

    public void OnStartBuffing()
    {
        StopAllCoroutines();
        audio.PlayOneShot(clips[4]);
        _animator.CrossFadeInFixedTime(BuffState, .2f);
    }

    public void OnStartDie()
    {
        _animator.CrossFadeInFixedTime(DieState, .2f);
    }

    public void OnStartHurt()
    {
        _animator.CrossFadeInFixedTime(HurtState, .2f);
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
        _playerproperty.StartHurt += OnStartHurt;
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
        _playerproperty.StartHurt -= OnStartHurt;
        _playerproperty.StartBuff -= OnStartBuffing;
        _playerproperty.StartDeath -= OnStartDie;
    }

}

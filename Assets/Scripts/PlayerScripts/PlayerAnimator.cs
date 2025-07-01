using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour
{
    private static readonly int _speedHash = Animator.StringToHash("Speed");
    private static readonly int _jumpHash = Animator.StringToHash("IsJumping");
    private static readonly int _deathHash = Animator.StringToHash("Death");

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetMoveSpeed(float speed)
    {
        _animator.SetFloat(_speedHash, speed);
    }

    public void SetJumpState(bool isJumping)
    {
        _animator.SetBool(_jumpHash, isJumping);
    }

    public void PlayDeath()
    {
        _animator.SetTrigger(_deathHash);
    }
}

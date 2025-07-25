using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour
{
    private static readonly int _speedHash = Animator.StringToHash("Speed");
    private static readonly int _jumpHash = Animator.StringToHash("IsJumping");
    private static readonly int _deathHash = Animator.StringToHash("Death");
    private static readonly int _punchRightHash = Animator.StringToHash("PunchRight");
    private static readonly int _punchLeftHash = Animator.StringToHash("PunchLeft");

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

    public void PlayPunch(float facingDirection)
    {
        _animator.ResetTrigger(_punchLeftHash);
        _animator.ResetTrigger(_punchRightHash);

        if (facingDirection > 0)
            _animator.SetTrigger(_punchRightHash);
        else
            _animator.SetTrigger(_punchLeftHash);
    }
}

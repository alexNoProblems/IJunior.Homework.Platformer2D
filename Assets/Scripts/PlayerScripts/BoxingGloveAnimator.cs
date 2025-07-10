using UnityEngine;

[RequireComponent(typeof(Animator))]
public class BoxingGloveAnimator : MonoBehaviour
{
    private static readonly int _punchRightHash = Animator.StringToHash("PunchRight");
    private static readonly int _punchLeftHash = Animator.StringToHash("PunchLeft");

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void PlayPunch(float facingDirection)
    {
        if (_animator == null)
            return;
        
        _animator.ResetTrigger(_punchLeftHash);
        _animator.ResetTrigger(_punchRightHash);

        if (facingDirection > 0)
            _animator.SetTrigger(_punchRightHash);
        else
            _animator.SetTrigger(_punchLeftHash);
    }
}

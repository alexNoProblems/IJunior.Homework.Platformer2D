using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyAnimator : MonoBehaviour
{
    private readonly int _speedHash = Animator.StringToHash("Speed");
    
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();    
    }

    public void UpdateSpeed(float speed)
    {
        if (_animator != null)
            _animator.SetFloat(_speedHash, speed);
    }
}

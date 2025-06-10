using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyAnimator : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _enemyRididbody2D;

    private readonly int _speedHash = Animator.StringToHash("Speed");
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _enemyRididbody2D = GetComponentInParent<Rigidbody2D>();
    }

    private void Update()
    {
        float speedX = Mathf.Abs(_enemyRididbody2D.velocity.x);
        _animator.SetFloat(_speedHash, Mathf.Abs(_enemyRididbody2D.velocity.x));
    }
}

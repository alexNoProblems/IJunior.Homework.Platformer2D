using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private PlayerJumper _playerJumper;

    private Animator _animator;
    private string _speed = "Speed";
    private string _isJumping = "IsJumping";

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        float speed = Mathf.Abs(_inputReader.Movement.x);

        _animator.SetFloat(_speed, speed);
        _animator.SetBool(_isJumping, _playerJumper.IsJumping);

        FlipSprite(_inputReader.Movement.x);
    }

    private void FlipSprite(float directionX)
    {
        if (directionX == 0) return;

        Vector3 localScale = transform.localScale;
        localScale.x = Mathf.Sign(directionX) * Mathf.Abs(localScale.x);
        transform.localScale = localScale;
    }
}

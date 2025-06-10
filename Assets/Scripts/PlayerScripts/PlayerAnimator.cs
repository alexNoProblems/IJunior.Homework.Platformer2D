using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private PlayerJumper _playerJumper;
    [SerializeField] private SpriteFlipper _spriteFlipper;

    private Animator _animator;
    private string _speed = "Speed";
    private string _isJumping = "IsJumping";

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        float horizontal = _inputReader.Movement.x;
        float speed = Mathf.Abs(horizontal);

        _animator.SetFloat(_speed, speed);
        _animator.SetBool(_isJumping, _playerJumper.IsJumping);

        _spriteFlipper.Flip(horizontal);
    }
}

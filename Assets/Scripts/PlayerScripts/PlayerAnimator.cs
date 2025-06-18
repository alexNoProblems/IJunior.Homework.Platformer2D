using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour
{
    private InputReader _inputReader;
    private PlayerJumper _playerJumper;
    private SpriteFlipper _spriteFlipper;
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

    public void Init(InputReader inputReader, PlayerJumper jumper, SpriteFlipper spriteFlipper)
    {
        _inputReader = inputReader;
        _playerJumper = jumper;
        _spriteFlipper = spriteFlipper;
    }
}

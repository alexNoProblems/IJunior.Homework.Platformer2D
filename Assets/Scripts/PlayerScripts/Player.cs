using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(PlayerMover), typeof(PlayerJumper), typeof(JumpSoundPlayer))]
[RequireComponent(typeof(PlayerCollector), typeof(SpriteFlipper))]
public class Player : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private GroundChecker _groundChecker;
    [SerializeField] private PlayerAnimator _playerAnimator;
    [SerializeField] private AudioClip _jumpSound;
    [SerializeField] private CoinCollectSoundPlayer _coinSoundPlayer;

    private PlayerMover _mover;
    private PlayerJumper _jumper;
    private PlayerCollector _collector;
    private JumpSoundPlayer _jumpSoundPlayer;
    private SpriteFlipper _spriteFlipper;

    private void Awake()
    {
        _mover = GetComponent<PlayerMover>();
        _jumper = GetComponent<PlayerJumper>();
        _collector = GetComponent<PlayerCollector>();
        _jumpSoundPlayer = GetComponent<JumpSoundPlayer>();
        _spriteFlipper = GetComponent<SpriteFlipper>();

        _jumper.Init(_groundChecker);
        _jumpSoundPlayer.Init(_jumper, _jumpSound);
        _collector.Init(_coinSoundPlayer);
    }

    private void FixedUpdate()
    {
        float horizontal = _inputReader.Movement.x;

        _mover.Move(_inputReader.Movement.x);

        if (_inputReader.JumpRequested && _jumper.IsGrounded)
            _jumper.Jump(horizontal);

        _playerAnimator.SetMoveSpeed(Mathf.Abs(horizontal));
        _playerAnimator.SetJumpState(_jumper.IsJumping);
        _spriteFlipper.Flip(horizontal);

        _inputReader.ClearJumpRequest();
    }
}

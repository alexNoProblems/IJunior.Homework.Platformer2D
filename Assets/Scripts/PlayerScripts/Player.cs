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
    [SerializeField] private Puncher _puncher;
    [SerializeField] private Glove _glove;
    [SerializeField] private Transform _gloveSpawnPoint;

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
        _puncher.Init(_glove, _gloveSpawnPoint);
    }

    private void FixedUpdate()
    {
        float horizontal = _inputReader.Movement.x;

        if (!_inputReader.PunchRequested)
        {
            _mover.Move(horizontal);

            if (horizontal != 0)
                _spriteFlipper.Flip(horizontal);
        }
        else
        {
            _mover.Move(0);
        }

        if (_inputReader.JumpRequested && _jumper.IsGrounded)
            _jumper.Jump(horizontal);

        _playerAnimator.SetMoveSpeed(Mathf.Abs(horizontal));
        _playerAnimator.SetJumpState(_jumper.IsJumping);

        _inputReader.ClearJumpRequest();

        if (_inputReader.PunchRequested)
        {
            int facingDirection = _spriteFlipper.FacingDirection;
            _puncher.TryPunch(facingDirection);
            _inputReader.ClearPunchRequest();
        }
    }
}

using UnityEngine;

[RequireComponent(typeof(PlayerMover), typeof(PlayerJumper), typeof(JumpSoundPlayer))]
[RequireComponent(typeof(PlayerCollector), typeof(SpriteFlipper), typeof(BoxingGloveSoundPlayer))]
[RequireComponent(typeof(Health))]
public class Player : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private GroundChecker _groundChecker;
    [SerializeField] private PlayerAnimator _playerAnimator;
    [SerializeField] private AudioClip _jumpSound;
    [SerializeField] private AudioClip _boxingGloveKickSound;
    [SerializeField] private CoinCollectSoundPlayer _coinSoundPlayer;
    [SerializeField] private Puncher _puncher;
    [SerializeField] private Glove _glove;
    [SerializeField] private Transform _gloveSpawnPoint;
    [SerializeField] private float _deathDelay = 2f;

    private PlayerMover _mover;
    private PlayerJumper _jumper;
    private PlayerCollector _collector;
    private JumpSoundPlayer _jumpSoundPlayer;
    private BoxingGloveSoundPlayer _boxingGloveSoundPlayer;
    private SpriteFlipper _spriteFlipper;
    private Health _health;

    private void Awake()
    {
        _mover = GetComponent<PlayerMover>();
        _jumper = GetComponent<PlayerJumper>();
        _collector = GetComponent<PlayerCollector>();
        _jumpSoundPlayer = GetComponent<JumpSoundPlayer>();
        _boxingGloveSoundPlayer = GetComponent<BoxingGloveSoundPlayer>();
        _spriteFlipper = GetComponent<SpriteFlipper>();
        _health = GetComponent<Health>();

        _jumper.Init(_groundChecker);
        _jumpSoundPlayer.Init(_jumper, _jumpSound);
        _boxingGloveSoundPlayer.Init(_puncher, _boxingGloveKickSound);
        _collector.Init(_coinSoundPlayer);
        _puncher.Init(_glove, _gloveSpawnPoint);
        _health.Init(OnDeath);
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

    private void OnDeath()
    {
        _playerAnimator.PlayDeath();

        Collider2D collider2D = GetComponent<Collider2D>();

        if (collider2D != null)
            collider2D.enabled = false;

        Destroy(gameObject, _deathDelay);
    }
}

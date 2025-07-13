using UnityEngine;

[RequireComponent(typeof(PlayerMover), typeof(PlayerJumper), typeof(JumpSoundPlayer))]
[RequireComponent(typeof(PlayerCollector), typeof(SpriteFlipper), typeof(BoxingGloveSoundPlayer))]
[RequireComponent(typeof(Health), typeof(PlayerKiller), typeof(DeathSoundPlayer))]
[RequireComponent(typeof(PlayerHealerHandler), typeof(GloveEnemyKiller))]
public class Player : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private GroundChecker _groundChecker;
    [SerializeField] private PlayerAnimator _playerAnimator;
    [SerializeField] private ItemsCollectSoundPlayer _coinSoundPlayer;
    [SerializeField] private Puncher _puncher;
    [SerializeField] private Glove _glove;
    [SerializeField] private CameraFollower _cameraFollower;
    [SerializeField] private Transform _gloveSpawnPoint;
    [SerializeField] private float _deathDelay = 2f;

    private PlayerMover _mover;
    private PlayerJumper _jumper;
    private PlayerCollector _collector;
    private JumpSoundPlayer _jumpSoundPlayer;
    private BoxingGloveSoundPlayer _boxingGloveSoundPlayer;
    private DeathSoundPlayer _deathSoundPlayer;
    private SpriteFlipper _spriteFlipper;
    private Health _health;
    private PlayerKiller _playerKiller;
    private PlayerHealerHandler _playerHealerHandler;

    private void Awake()
    {
        _mover = GetComponent<PlayerMover>();
        _jumper = GetComponent<PlayerJumper>();
        _collector = GetComponent<PlayerCollector>();
        _jumpSoundPlayer = GetComponent<JumpSoundPlayer>();
        _boxingGloveSoundPlayer = GetComponent<BoxingGloveSoundPlayer>();
        _deathSoundPlayer = GetComponent<DeathSoundPlayer>();
        _spriteFlipper = GetComponent<SpriteFlipper>();
        _health = GetComponent<Health>();
        _playerKiller = GetComponent<PlayerKiller>();
        _playerHealerHandler = GetComponent<PlayerHealerHandler>();

        _jumper.Init(_groundChecker);
        _jumpSoundPlayer.Init(_jumper);
        _boxingGloveSoundPlayer.Init(_puncher);
        _deathSoundPlayer.Init(_health);
        _collector.Init(_coinSoundPlayer);
        _puncher.Init(_glove, _gloveSpawnPoint);
        _playerKiller.Init(_playerAnimator, _deathDelay);
        _playerHealerHandler.Init(_coinSoundPlayer);

        _health.Died += OnDeath;
    }

    private void FixedUpdate()
    {
        if (_health.IsDead)
            return;

        HandleMovement();
        HandleJump();
        HandlePunch();
    }

    private void OnDestroy()
    {
        if (_health != null)
            _health.Died -= OnDeath;  
    }

    private void HandleMovement()
    {
        float horizontal = _inputReader.Movement.x;

        if (_inputReader.PunchRequested)
        {
            _mover.Move(0);
        }
        else
        {
            _mover.Move(horizontal);

            if (Mathf.Approximately(horizontal, 0f) == false)
                _spriteFlipper.FlipHorizontal(horizontal);
        }
        
        _playerAnimator.SetMoveSpeed(Mathf.Abs(horizontal));
        _playerAnimator.SetJumpState(_jumper.IsJumping);
    }

    private void HandleJump()
    {
        if (_inputReader.JumpRequested && _jumper.IsGrounded)
            _jumper.Jump(_inputReader.Movement.x);
        
        _inputReader.ClearJumpRequest();
    }

    private void HandlePunch()
    {
        if (_inputReader.PunchRequested)
        {
            int facingDirection = _spriteFlipper.FacingDirection;

            _puncher.TryPunch(facingDirection);
            _inputReader.ClearPunchRequest();
        }
    }

    private void OnDeath()
    {
        _playerKiller.Die();
        _cameraFollower.SetTarget(null);
    }
}

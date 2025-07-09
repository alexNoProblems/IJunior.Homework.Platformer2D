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
    [SerializeField] private AudioClip _jumpSound;
    [SerializeField] private AudioClip _boxingGloveKickSound;
    [SerializeField] private AudioClip _deathSound;
    [SerializeField] private ItemsCollectSoundPlayer _coinSoundPlayer;
    [SerializeField] private Puncher _puncher;
    [SerializeField] private Glove _glove;
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
    private bool _isDead;

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
        _jumpSoundPlayer.Init(_jumper, _jumpSound);
        _boxingGloveSoundPlayer.Init(_puncher, _boxingGloveKickSound);
        _deathSoundPlayer.Init(_playerKiller, _deathSound);
        _collector.Init(_coinSoundPlayer);
        _puncher.Init(_glove, _gloveSpawnPoint);
        _playerKiller.Init(_playerAnimator, _deathDelay);
        _playerHealerHandler.Init(_coinSoundPlayer);

        _health.OnDeath += OnDeath;
    }

    private void FixedUpdate()
    {
        if (_isDead)
            return;

        HandleMovement();
        HandleJump();
        HandlePunch();
    }

    private void OnDestroy()
    {
        if (_health != null)
            _health.OnDeath -= OnDeath;  
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
                _spriteFlipper.FlipRightLeft(horizontal);
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

            GloveEnemyKiller killer = _glove.GetComponent<GloveEnemyKiller>();

            if (killer != null)
            {
                Vector2 punchDirection = new Vector2(facingDirection, 0f);
                killer.SetKnockDirection(punchDirection);
            }

            _puncher.TryPunch(facingDirection);
            _inputReader.ClearPunchRequest();
        }
    }

    private void OnDeath()
    {
        _isDead = true;
        _playerKiller.Die();
    }
}

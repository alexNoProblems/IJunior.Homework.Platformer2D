using UnityEngine;

[RequireComponent(typeof(EnemyMover), typeof(SpriteFlipper))]
public class EnemyChaser : MonoBehaviour
{
    [SerializeField] private float _chaseSpeed = 3f;
    [SerializeField] private float _losePlayerDistance = 4f;
    [SerializeField] private PlayerDetector _playerDetector;

    private EnemyMover _mover;
    private SpriteFlipper _spriteFlipper;
    private Transform _targetPlayer;
    private float _losePlayerDistanceSqr;

    public bool IsChasing => _targetPlayer != null;
    public Transform TargetPlayer => _targetPlayer;

    private void Awake()
    {
        _mover = GetComponent<EnemyMover>();
        _spriteFlipper = GetComponent<SpriteFlipper>();
        _losePlayerDistanceSqr = _losePlayerDistance * _losePlayerDistance;
    }

    private void FixedUpdate()
    {
        if (_targetPlayer == null)
        {
            _targetPlayer = _playerDetector.TryDetectPlayer(transform.position);

            if (_targetPlayer != null)
            {
                float directionToPlayer = _targetPlayer.position.x - transform.position.x;
                _spriteFlipper.FlipHorizontal(directionToPlayer);
            }
        }
        else
        {
            if (!_targetPlayer.gameObject.activeInHierarchy || (_targetPlayer.position - transform.position).sqrMagnitude > _losePlayerDistanceSqr)
            {
                _targetPlayer = null;

                return;
            }

            ChasePlayer();
        }
    }

    private void ChasePlayer()
    {
        Vector2 direction = (_targetPlayer.position - transform.position).normalized;
        Vector2 velocity = new Vector2(direction.x * _chaseSpeed, _mover.CurrentYVelocity);

        _mover.Move(velocity);
    }
}

using UnityEngine;

[RequireComponent(typeof(EnemyMover), typeof(SpriteFlipper))]
public class EnemyChaser : MonoBehaviour
{
    [SerializeField] private float _chaseSpeed = 3f;
    [SerializeField] private float _detectionDistance = 3f;
    [SerializeField] private float _losePlayerDistance = 4f;
    [SerializeField] private Vector2 _detectionBoxSize = new Vector2(2f, 2f);

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
            TryDetectedPlayer();
        }
        else
        {
            if (!_targetPlayer.gameObject.activeInHierarchy || (_targetPlayer.position - transform.position).sqrMagnitude > _losePlayerDistanceSqr)
            {
                _targetPlayer = null;

                return;
            }
        }

        ChasePlayer();
    }

    private void TryDetectedPlayer()
    {
        TryDetectInDirection(Vector2.right);
        TryDetectInDirection(Vector2.left);
    }

    private void TryDetectInDirection(Vector2 direction)
    {
        float halfDetectionDistance = _detectionDistance / 2;
        Vector2 origin = (Vector2)transform.position + direction * halfDetectionDistance;

        RaycastHit2D hit = Physics2D.BoxCast(origin, _detectionBoxSize, 0f, direction, 0f);

        if (hit.collider != null)
        {
            Player player = hit.collider.GetComponent<Player>();

            if (player != null && player.gameObject.activeInHierarchy)
            {
                _targetPlayer = player.transform;

                float directionToPlayer = _targetPlayer.position.x - transform.position.x;
                _spriteFlipper.FlipRightLeft(directionToPlayer);

                return;
            }
        }
    }

    private void ChasePlayer()
    {
        if (_targetPlayer == null)
            return;

        Vector2 direction = (_targetPlayer.position - transform.position).normalized;
        Vector2 velocity = new Vector2(direction.x * _chaseSpeed, _mover.CurrentYVelocity);

        _mover.Move(velocity);
    }
}

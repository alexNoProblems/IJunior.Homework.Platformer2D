using UnityEngine;
using System.Collections;

[RequireComponent(typeof(EnemyMover))]
public class EnemyPatroller : MonoBehaviour
{
    private const int FlipMultiplier = -1;

    [SerializeField] private float _moveSpeed = 2f;
    [SerializeField] private float _patrolDuration = 10f;
    [SerializeField] private float _waitDuration = 1f;
    [SerializeField] private GroundChecker _groundChecker;

    private EnemyMover _mover;
    private WaitForSeconds _waitForSeconds;
    private bool _isMoving = true;
    private float _patrolTimer = 0f;
    private int _direction = -1;

    private void Start()
    {
        _mover = GetComponent<EnemyMover>();
        _waitForSeconds = new WaitForSeconds(_waitDuration);
    }

    private void FixedUpdate()
    {
        if (!_isMoving)
            return;
            
        _patrolTimer += Time.deltaTime;

        _mover.Move(new Vector2(_direction * _moveSpeed, _mover.CurrentYVelocity));

        if (_patrolTimer >= _patrolDuration || _groundChecker.IsWallAhead(_direction))
            StartCoroutine(WaitAndTurn());
    }

    private IEnumerator WaitAndTurn()
    {
        _isMoving = false;
        _mover.Stop();

        yield return _waitForSeconds;

        Flip();
        _patrolTimer = 0f;
        _isMoving = true;
    }

    private void Flip()
    {
        _direction *= FlipMultiplier;
        Vector3 lockalScale = transform.localScale;
        lockalScale.x *= FlipMultiplier;
        transform.localScale = lockalScale;
    }
}

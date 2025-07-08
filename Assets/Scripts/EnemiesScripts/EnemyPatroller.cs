using UnityEngine;
using System.Collections;

[RequireComponent(typeof(EnemyMover), typeof(Enemy), typeof(EnemyChaser))]
public class EnemyPatroller : MonoBehaviour
{
    private const int FlipMultiplier = -1;

    [SerializeField] private float _moveSpeed = 2f;
    [SerializeField] private float _patrolDuration = 5f;
    [SerializeField] private float _waitDuration = 1f;
    [SerializeField] private WallChecker _wallChecker;
    [SerializeField] private SpriteFlipper _spriteFlipper;

    private EnemyMover _mover;
    private WaitForSeconds _waitForSeconds;
    private EnemyChaser _enemyChaser;
    private bool _isMoving = true;
    private float _patrolTimer = 0f;
    private int _direction = 1;

    private void Start()
    {
        _mover = GetComponent<EnemyMover>();
        _enemyChaser = GetComponent<EnemyChaser>();
        _waitForSeconds = new WaitForSeconds(_waitDuration);
    }

    private void FixedUpdate()
    {
        Enemy enemy = GetComponent<Enemy>();

        if (enemy != null && enemy.IsDying)
            return;
        
        if (_enemyChaser != null && _enemyChaser.IsChasing)
            return;
        
        if (!_isMoving)
            return;
        
        _patrolTimer += Time.deltaTime;
        _mover.Move(new Vector2(_direction * _moveSpeed, _mover.CurrentYVelocity));

        if (_enemyChaser != null && !_enemyChaser.IsChasing)
            _spriteFlipper.FlipRightLeft(_direction);
        
        if (_patrolTimer >= _patrolDuration || _wallChecker.IsWallAhead(_direction))
        {
            _isMoving = false;
            StartCoroutine(WaitAndTurn());
        }
    }

    private IEnumerator WaitAndTurn()
    {
        _mover.Stop();

        yield return _waitForSeconds;

        _direction *= -1;
        _spriteFlipper.FlipRightLeft(_direction);
        _patrolTimer = 0f;
        _isMoving = true;
    }
}

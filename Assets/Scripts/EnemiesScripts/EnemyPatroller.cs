using UnityEngine;
using System.Collections;

[RequireComponent(typeof(EnemyMover))]
public class EnemyPatroller : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 2f;
    [SerializeField] private float _patrolDuration = 5f;
    [SerializeField] private float _waitDuration = 1f;
    [SerializeField] private WallChecker _wallChecker;
    [SerializeField] private SpriteFlipper _spriteFlipper;

    private EnemyMover _mover;
    private float _timer = 0f;
    private float _waitTimer = 0f;
    private int _direction = 1;
    private bool _isWaiting = false;

    private void Awake()
    {
        _mover = GetComponent<EnemyMover>();
    }

    public void Patrol(float deltaTime)
    {
        if (_isWaiting == true)
        {
            _waitTimer += deltaTime;

            if (_waitTimer >= _waitDuration)
            {
                _isWaiting = false;
                _waitTimer = 0f;
                _direction *= -1;
                _spriteFlipper.FlipHorizontal(_direction);
            }

            return;
        }

        _timer += deltaTime;
        _mover.Move(new Vector2(_direction * _moveSpeed, _mover.CurrentYVelocity));
        _spriteFlipper.FlipHorizontal(_direction);

        if (_timer >= _patrolDuration || _wallChecker.IsWallAhead(_direction))
        {
            _timer = 0f;
            _mover.Stop();
            _isWaiting = true;
        }
    }
}

using UnityEngine;

[RequireComponent(typeof(EnemyMover), typeof(SpriteFlipper), typeof(EnemyPatroller))]
[RequireComponent(typeof(EnemyChaser), typeof(Health))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private WallChecker _wallChecker;
    [SerializeField] private EnemyAnimator _enemyAnimator;
    [SerializeField] private float _deathDelay = 0.5f;

    private EnemyMover _mover;
    private EnemyPatroller _patroller;
    private EnemyChaser _chaser;
    private SpriteFlipper _spriteFlipper;
    private Health _health;
    private bool _isDying = false;

    public bool IsDying => _isDying;

    private void Awake()
    {
        _health = GetComponent<Health>();
        _health.Died += Die;

        _mover = GetComponent<EnemyMover>();
        _patroller = GetComponent<EnemyPatroller>();
        _chaser = GetComponent<EnemyChaser>();
        _spriteFlipper = GetComponent<SpriteFlipper>();
    }

    private void FixedUpdate()
    {
        if (_isDying == true)
            return;

        float speedX = Mathf.Abs(_mover.Velocity.x);

        _enemyAnimator.UpdateSpeed(speedX);

        if (_chaser != null && _chaser.IsChasing)
            return;
        
        if (_patroller != null)
        {
            _patroller.Patrol(Time.fixedDeltaTime);
        }
    }
    
    private void OnDestroy()
    {
        if (_health != null)
            _health.Died -= Die;
    }

    public void Die()
    {
        _isDying = true;
        _mover.Stop();

        if (_spriteFlipper != null)
            _spriteFlipper.FlipVertical();

        Collider2D collider = GetComponent<Collider2D>();

        if (collider != null)
            collider.enabled = false;

        Destroy(gameObject, _deathDelay);
    }
}

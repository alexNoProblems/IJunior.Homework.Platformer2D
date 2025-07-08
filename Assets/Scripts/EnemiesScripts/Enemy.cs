using UnityEngine;

[RequireComponent(typeof(EnemyMover), typeof(SpriteFlipper))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private WallChecker _wallChecker;
    [SerializeField] private EnemyAnimator _enemyAnimator;
    [SerializeField] private float _deathDelay = 0.5f;

    private EnemyMover _mover;
    private bool _isDying = false;

     public bool IsDying => _isDying;

    private void Awake()
    {
        _mover = GetComponent<EnemyMover>();
    }

    private void FixedUpdate()
    {
        float speedX = Mathf.Abs(_mover.Velocity.x);

        _enemyAnimator.UpdateSpeed(speedX);
    }

    public void Die()
    {
        _mover.Stop();

        SpriteFlipper spriteFlipper = GetComponent<SpriteFlipper>();

        if (spriteFlipper != null)
            spriteFlipper.FlipUpsideDown();

        Collider2D collider = GetComponent<Collider2D>();

        if (collider != null)
            collider.enabled = false;

        Destroy(gameObject, _deathDelay);
    }
}

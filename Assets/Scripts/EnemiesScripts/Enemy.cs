using UnityEngine;

[RequireComponent(typeof(EnemyMover), typeof(SpriteFlipper))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private WallChecker _wallChecker;
    [SerializeField] private EnemyAnimator _enemyAnimator;

    private EnemyMover _mover;

    private void Awake()
    {
        _mover = GetComponent<EnemyMover>();
    }

    private void FixedUpdate()
    {
        float speedX = Mathf.Abs(_mover.Velocity.x);

        _enemyAnimator.UpdateSpeed(speedX);
    }
}

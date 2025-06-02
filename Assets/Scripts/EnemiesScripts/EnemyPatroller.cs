using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyPatroller : MonoBehaviour
{
    private const int FlipMultiplier = -1;

    [SerializeField] private float _moveSpeed = 2f;
    [SerializeField] private float _patrolDuration = 10f;
    [SerializeField] private float _waitDuration = 1f;
    [SerializeField] private float _checkDistance = 0.2f;
    [SerializeField] private Transform _wallChecker;

    private Rigidbody2D _rigidbody2D;
    private WaitForSeconds _waitForSeconds;
    private bool _isMoving = true;
    private float _patrolTimer = 0f;
    private int _direction = -1;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _waitForSeconds = new WaitForSeconds(_waitDuration);
    }

    private void FixedUpdate()
    {
        if (_isMoving)
        {
            _patrolTimer += Time.deltaTime;

            _rigidbody2D.velocity = new Vector2(_direction * _moveSpeed, _rigidbody2D.velocity.y);

            RaycastHit2D hit = Physics2D.Raycast(_wallChecker.position, Vector2.right * _direction, _checkDistance);
            bool hitGround = hit.collider != null && hit.collider.GetComponent<Ground>() != null;

            if (_patrolTimer >= _patrolDuration || hit.collider != null)
                StartCoroutine(WaitAndTurn());
        }
    }

    private IEnumerator WaitAndTurn()
    {
        _isMoving = false;
        _rigidbody2D.velocity = Vector2.zero;

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

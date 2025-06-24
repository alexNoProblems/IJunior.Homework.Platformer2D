using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(GroundChecker))]
public class PlatformPassenger : MonoBehaviour
{
    private IMovingPlatform _currentPlatform;
    private GroundChecker _groundChecker;
    private Rigidbody2D _rigidbody2D;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _groundChecker = GetComponent<GroundChecker>();
    }

    private void FixedUpdate()
    {
        if (_currentPlatform != null && _groundChecker != null && _groundChecker.IsGrounded())
        {
            Vector2 velocity = _rigidbody2D.velocity;
            velocity.y += _currentPlatform.DeltaMovement.y / Time.fixedDeltaTime;
            _rigidbody2D.velocity = velocity;
        }
    }

    public void OnEnterCollision2D(Collision2D collision) =>
        HandleCollision(collision);

    public void OnStayCollision2D(Collision2D collision) =>
        HandleCollision(collision);

    public void OnExitCollision2D(Collision2D collision)
    {
        if (_currentPlatform != null && collision.collider.TryGetComponent<IMovingPlatform>(out var platform))
            _currentPlatform = platform;
    }

    private void HandleCollision(Collision2D collision)
    {
        if (_currentPlatform == null)
            _currentPlatform = collision.collider.GetComponent<MovingPlatform>();
    }
}

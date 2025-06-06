using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(PlayerJumper))]
public class PlatformPassenger : MonoBehaviour, ICollisionHandler2D
{
    private MovingPlatform _currentPlatform;
    private PlayerJumper _playerJumper;
    private Rigidbody2D _rigidbody2D;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _playerJumper = GetComponent<PlayerJumper>();
    }

    public void OnEnterCollision2D(Collision2D collision)
    {
        TryAssignPlatform(collision);
    }

    public void OnStayCollision2D(Collision2D collision)
    {
        TryAssignPlatform(collision);
    }

    public void OnExitCollision2D(Collision2D collision)
    {
        if (_currentPlatform != null && collision.collider.GetComponent<MovingPlatform>() == _currentPlatform)
            _currentPlatform = null;
    }

    private void TryAssignPlatform(Collision2D collision)
    {
        if (_currentPlatform == null)
            _currentPlatform = collision.collider.GetComponent<MovingPlatform>();
    }

    private void FixedUpdate()
    {
        if (_currentPlatform != null && !_playerJumper.IsJumping)
        {
            Vector2 velocity = _rigidbody2D.velocity;
            velocity.y += _currentPlatform.DeltaMovement.y / Time.fixedDeltaTime;
            _rigidbody2D.velocity = velocity;
        }
    }
}

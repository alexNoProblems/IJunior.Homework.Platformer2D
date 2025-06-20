using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMover : MonoBehaviour
{
    public float CurrentYVelocity => _rigidbody2D.velocity.y;
    public Vector2 Velocity => _rigidbody2D.velocity;
    
    private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void Move(Vector2 velocity)
    {
        _rigidbody2D.velocity = velocity;
    }

    public void Stop()
    {
        _rigidbody2D.velocity = Vector2.zero;
    }
}

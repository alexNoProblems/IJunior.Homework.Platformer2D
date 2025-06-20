using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _speed = 7f;

    private Rigidbody2D _rigidBody2D;

    private void Awake()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
    }

    public void Move(float directionX)
    {
        Vector2 velocity = _rigidBody2D.velocity;
        velocity.x = directionX * _speed;
        _rigidBody2D.velocity = velocity;
    }
}

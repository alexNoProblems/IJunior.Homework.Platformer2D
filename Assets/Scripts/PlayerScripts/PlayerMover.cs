using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private float _speed = 7f;

    public float Speed => _speed;
    public float MovementX => _inputReader.Movement.x;
    private Rigidbody2D _rigidBody2D;

    private void Start()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Vector2 velocity =_rigidBody2D.velocity;
        velocity.x = _inputReader.Movement.x * _speed;
        _rigidBody2D.velocity = velocity;
    }
}

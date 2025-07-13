using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerJumper : MonoBehaviour
{
    public bool IsJumping { get; private set; }
    public bool IsGrounded { get; private set; }

    public event Action Jumped;

    [SerializeField] private float _jumpForce = 13f;
    [SerializeField] private float _jumpDirectionY = 1;
    [SerializeField] private float _jumpHorizontalFactor = 2f;

    private GroundChecker _groundChecker;
    private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        IsGrounded = _groundChecker.IsGrounded();
        IsJumping = !IsGrounded;
    }

    public void Init(GroundChecker groundChecker)
    {
        _groundChecker = groundChecker;
    }

    public void Jump(float horizontalInput)
    {
        if (!IsGrounded)
            return;

        IsJumping = true;

        _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, 0f);

        float horizontalBoost = horizontalInput * _jumpHorizontalFactor;
        Vector2 jumpVector = new Vector2(horizontalBoost, _jumpDirectionY);

        _rigidbody2D.AddForce(jumpVector * _jumpForce, ForceMode2D.Impulse);

        Jumped?.Invoke();
    }
}

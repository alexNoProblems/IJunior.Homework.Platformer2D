using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerJumper : MonoBehaviour
{
    public bool IsJumping { get; private set; }

    [SerializeField] private InputReader _inputReader;
    [SerializeField] private Transform _groundChecker;
    [SerializeField] private float _rayDistance = 0.2f;
    [SerializeField] private float _jumpForce = 13f;
    [SerializeField] private float _jumpDirectionY = 1;
    [SerializeField] private float _jumpHorizontalFactor = 2f;

    private Rigidbody2D _rigidbody2D;
    private bool _isGrounded;
    private bool _shouldJump;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        StateUpdate();
        
        if (_inputReader.IsJumpPressed && _isGrounded)
            _shouldJump = true;
    }

    private void FixedUpdate()
    {
        if (_shouldJump)
        {
            IsJumping = true;

            float horizontalBoost = _inputReader.Movement.x * _jumpHorizontalFactor;
            Vector2 jumpVector = new Vector2(horizontalBoost, _jumpDirectionY);
            _rigidbody2D.AddForce(jumpVector * _jumpForce, ForceMode2D.Impulse);

            _shouldJump = false;
        }

        IsJumping = !_isGrounded;
    }

    public bool IsGrounded()
    {
        return _isGrounded;
    }

    private void StateUpdate()
    {
        RaycastHit2D raycastHit2D = Physics2D.Raycast(_groundChecker.position, Vector2.down, _rayDistance);

        if (raycastHit2D.collider != null)
            _isGrounded = raycastHit2D.collider.GetComponent<Ground>() != null;
        else
            _isGrounded = false;
    }
}

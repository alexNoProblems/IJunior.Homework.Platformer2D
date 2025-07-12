using UnityEngine;

public class SpriteFlipper : MonoBehaviour
{
    private const float RightRotationY = 0f;
    private const float LeftRotationY = 180f;
    private const float UpsideDownRotationZ = 180f;
    
    private Quaternion _facingRight;
    private Quaternion _facingLeft;
    private bool _isFacingRight = true;

    private void Awake()
    {
        _facingRight = Quaternion.Euler(0, 0, 0);
        _facingLeft = Quaternion.Euler(0, 180, 0);
    }

    public void FlipVertical()
    {
        float yRotation = _isFacingRight ? RightRotationY : LeftRotationY;
        transform.rotation = Quaternion.Euler(0f, yRotation, UpsideDownRotationZ);
    }

    public void FlipHorizontal(float directionX)
    {
        if (Mathf.Approximately(directionX, 0f))
            return;

        bool shouldFaceRight = directionX > 0f;

        if (_isFacingRight == shouldFaceRight)
            return;

        _isFacingRight = shouldFaceRight;
        transform.rotation = directionX > 0f ? _facingRight : _facingLeft;
    }

    public int FacingDirection =>
        _isFacingRight ? 1 : -1;
}

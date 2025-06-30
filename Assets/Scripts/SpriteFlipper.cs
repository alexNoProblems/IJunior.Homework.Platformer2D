using UnityEngine;

public class SpriteFlipper : MonoBehaviour
{
    private Quaternion _facingRight;
    private Quaternion _facingLeft;
    private bool _isFacingRight = true;

    private void Awake()
    {
        _facingRight = Quaternion.Euler(0, 0, 0);
        _facingLeft = Quaternion.Euler(0, 180, 0);
    }

    public void Flip(float directionX)
    {
        if (Mathf.Approximately(directionX, 0f))
            return;

        _isFacingRight = directionX > 0f;
        transform.rotation = directionX > 0f ? _facingRight : _facingLeft;
    }

    public int FacingDirection =>
        _isFacingRight ? 1 : -1;
}

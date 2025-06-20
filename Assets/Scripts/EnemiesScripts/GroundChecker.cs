using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    [SerializeField] private Transform _groundCheckPoint;
    [SerializeField] private float _checkDistance = 0.2f;

    public bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(_groundCheckPoint.position, Vector2.down, _checkDistance);

        return hit.collider != null &&
               (hit.collider.GetComponent<Ground>() != null ||
                hit.collider.GetComponent<IMovingPlatform>() != null);
    }
}

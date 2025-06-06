using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    [SerializeField] private Transform _wallChecker;
    [SerializeField] private float _checkDistance = 0.2f;

    public bool IsWallAhead(int direction)
    {
        RaycastHit2D hit = Physics2D.Raycast(_wallChecker.position, Vector2.right * direction, _checkDistance);

        return hit.collider != null && hit.collider.GetComponent<Ground>() != null;
    }
}

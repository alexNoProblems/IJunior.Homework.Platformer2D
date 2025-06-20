using UnityEngine;

public class WallChecker : MonoBehaviour
{
    [SerializeField] private Transform _wallCheckPoint;
    [SerializeField] private float _checkDistance = 0.2f;

    public bool IsWallAhead(int direction)
    {
        if (_wallCheckPoint != null)
        {
            Vector2 origin = _wallCheckPoint.position;
            Vector2 rayDirection = Vector2.right * direction;

            RaycastHit2D hit = Physics2D.Raycast(origin, rayDirection, _checkDistance);

            return hit.collider != null && hit.collider.GetComponent<Ground>() != null;
        }
        else
        {
            return false;
        }
    }
}

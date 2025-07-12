using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    [SerializeField] private float _detectionDistance = 3f;
    [SerializeField] private Vector2 _detectionBoxSize = new Vector2(3f, 3f);

    public Transform TryDetectPlayer(Vector2 enemyPosition)
    {
        Transform player = TryDetectInDirection(enemyPosition, Vector2.right);

        if (player == null)
            player = TryDetectInDirection(enemyPosition, Vector2.left);

        return player;
    }

    private Transform TryDetectInDirection(Vector2 enemyPosition, Vector2 direction)
    {
        float halfDistance = _detectionDistance / 2;
        Vector2 origin = enemyPosition + direction * halfDistance;

        RaycastHit2D hit = Physics2D.BoxCast(origin, _detectionBoxSize, 0f, direction, 0f);

        if (hit.collider != null)
        {
            Player player = hit.collider.GetComponent<Player>();

            if (player != null && player.gameObject.activeInHierarchy)
                return player.transform;
        }

        return null;
    }
}

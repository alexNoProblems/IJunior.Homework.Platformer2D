using UnityEngine;

public class ClosestEnemyFinder: MonoBehaviour
{
    public Enemy FindClosestEnemy(Vector2 origin, float range)
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(origin, range);
        Enemy closestEnemy = null;
        float closestSqrDistance = float.MaxValue;

        foreach (Collider2D hit in hits)
        {
            if (hit.TryGetComponent(out Enemy enemy))
            {
                float sqrDistance = ((Vector2)hit.transform.position - origin).sqrMagnitude;

                if (sqrDistance < closestSqrDistance)
                {
                    closestSqrDistance = sqrDistance;
                    closestEnemy = enemy;
                }
            }
        }

        return closestEnemy;   
    }
}

using UnityEngine;

public class ClosestEnemyFinder
{
    public static Enemy FindClosestEnemy(Vector2 origin, float range)
    {
        Enemy[] enemies = Object.FindObjectsOfType<Enemy>();
        Enemy closestEnemy = null;
        float closestSqrDistance = float.MaxValue;
        float sqrRange = range * range;

        foreach (Enemy enemy in enemies)
        {
            if (enemy.TryGetComponent(out Health health) && !health.IsDead)
            {
                float sqrDistance = ((Vector2)enemy.transform.position - origin).sqrMagnitude;

                if (sqrDistance < sqrRange && sqrDistance < closestSqrDistance)
                {
                    closestEnemy = enemy;
                    closestSqrDistance = sqrDistance;
                }
            }
        }

        return closestEnemy;   
    }
}

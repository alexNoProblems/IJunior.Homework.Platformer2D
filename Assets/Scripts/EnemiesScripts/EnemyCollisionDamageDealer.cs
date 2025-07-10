using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class EnemyCollisionDamageDealer : MonoBehaviour, ICollisionHandler2D
{
    [SerializeField] private int _damage = 1;

    public void OnEnterCollision2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Health>(out Health targetHealth))
            targetHealth.TakeDamage(_damage);
    }
}

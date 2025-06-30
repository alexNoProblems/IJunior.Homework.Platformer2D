using UnityEngine;

[RequireComponent(typeof(Health))]
public class PlayerCollisionDamageHandler : MonoBehaviour, ICollisionHandler2D
{
    [SerializeField] private int _damage = 1;

    private Health _health;

    private void Awake()
    {
        _health = GetComponent<Health>();
    }

    public void OnEnterCollision2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Enemy>(out _))
            _health.TakeDamage(_damage);
    }
}

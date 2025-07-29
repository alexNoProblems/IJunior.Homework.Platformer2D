using UnityEngine;
using System.Collections;

public class GloveEnemyKiller : MonoBehaviour, ITriggerHandler2D
{
    [SerializeField] private float _knockBackForce = 30f;
    [SerializeField] private float _knockBackVerticalFactor = 0.5f;
    [SerializeField] private float _deathDelay = 2f;
    [SerializeField] private int _damage = 1;

    private Vector2 _knockDirection = Vector2.right;
    private WaitForSeconds _waitForSeconds;

    private void Awake()
    {
        _waitForSeconds = new WaitForSeconds(_deathDelay);
    }

    public void PrepareForAttack(Vector2 direction)
    {
        _knockDirection = direction.normalized;
    }

    public void HandleTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.TryGetComponent<Health>(out Health health))
        {
            health.TakeDamage(_damage);
            KnockBack(health.GetComponent<Enemy>());
            StartCoroutine(DieAfterDelay(health.GetComponent<Enemy>()));
        }
    }

    public void SetKnockDirection(Vector2 direction)
    {
        _knockDirection = direction.normalized;
    }

    private IEnumerator DieAfterDelay(Enemy enemy)
    {
        yield return _waitForSeconds;

        enemy.Die();
    }

    private void KnockBack(Enemy enemy)
    {
        if (enemy.TryGetComponent<Rigidbody2D>(out Rigidbody2D rigidbody2D))
        {
            Vector2 knockBack = new Vector2(_knockDirection.x, _knockBackVerticalFactor).normalized * _knockBackForce;

            rigidbody2D.AddForce(knockBack, ForceMode2D.Impulse);
        }
    }
}

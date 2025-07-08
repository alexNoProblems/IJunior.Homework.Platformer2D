using UnityEngine;
using System.Collections;

public class GloveEnemyKiller : MonoBehaviour, ITriggerHandler2D
{
    [SerializeField] private float _knockBackForce = 30f;
    [SerializeField] private float _knockBackVerticalFactor = 0.5f;
    [SerializeField] private float _deathDelay = 2f;

    private Vector2 _knockDirection = Vector2.right;
    private WaitForSeconds _waitForSeconds;

    private void Awake()
    {
        _waitForSeconds = new WaitForSeconds(_deathDelay);
    }

    public void HandleTriggerEnter2D(Collider2D collider2D)
    {
        Enemy enemy = collider2D.GetComponent<Enemy>();

        if (enemy != null)
        {
            KnockBack(enemy);
            StartCoroutine(DieAfterDelay(enemy));
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
        Vector2 knockBack = new Vector2(_knockDirection.x, _knockBackVerticalFactor).normalized * _knockBackForce;

        EnemyMover mover = enemy.GetComponent<EnemyMover>();

        if (mover != null)
            mover.AddForce(knockBack);
    }
}

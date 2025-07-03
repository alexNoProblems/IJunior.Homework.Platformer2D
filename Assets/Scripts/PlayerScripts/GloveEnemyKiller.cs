using UnityEngine;
using System.Collections;

public class GloveEnemyKiller : MonoBehaviour, ITriggerHandler2D
{
    [SerializeField] private float _knockBackForce = 30f;
    [SerializeField] private float _knockBackVerticalFactor = 0.5f;
    [SerializeField] private float _deathDelay = 0.5f;

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
            StartCoroutine(KillWithDelay(enemy));
        }
    }

    private void KnockBack(Enemy enemy)
    {
        Vector2 direction = (enemy.transform.position - transform.position).normalized;
        Vector2 knockBack = new Vector2(direction.x, _knockBackVerticalFactor).normalized * _knockBackForce;

        EnemyMover mover = enemy.GetComponent<EnemyMover>();

        if (mover != null)
            mover.AddForce(knockBack);
    }

    private IEnumerator KillWithDelay(Enemy enemy)
    {
        yield return _waitForSeconds;

        if (enemy != null)
            enemy.Die();
    }
}

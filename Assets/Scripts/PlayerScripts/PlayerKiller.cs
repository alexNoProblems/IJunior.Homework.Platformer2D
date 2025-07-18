using System;
using System.Collections;
using UnityEngine;

public class PlayerKiller : MonoBehaviour
{
    private PlayerAnimator _animator;
    private WaitForSeconds _waitForSeconds;
    private float _deathDelay;

    private void Awake()
    {
        _waitForSeconds = new WaitForSeconds(_deathDelay);
    }

    public void Init(PlayerAnimator animator, float deathDelay)
    {
        _animator = animator;
        _deathDelay = deathDelay;
        _waitForSeconds = new WaitForSeconds(_deathDelay);
    }

    public void Die()
    {
        _animator.PlayDeath();

        StartCoroutine(HandleDeathSequence());
    }

    private IEnumerator HandleDeathSequence()
    {
        yield return _waitForSeconds;

        Collider2D collider2D = GetComponent<Collider2D>();

        if (collider2D != null)
            collider2D.enabled = false;
        
        Destroy(gameObject);
    }
}

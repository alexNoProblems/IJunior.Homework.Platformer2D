using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Health))]
public class DamageFlasher : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private float _duration = 2f;
    [SerializeField] private float _flashInterval = 0.1f;

    private Coroutine _flashCoroutine;
    private WaitForSeconds _waitForSeconds;
    private bool _isDead = false;

    private void Awake()
    {
        _waitForSeconds = new WaitForSeconds(_flashInterval);

        Health health = GetComponent<Health>();
        health.OnDamaged += Flash;
        health.OnDeath += StopFlashing;
    }

    public void Flash()
    {
        if (_isDead)
            return;
        
        if (_flashCoroutine != null)
            StopCoroutine(_flashCoroutine);

        _flashCoroutine = StartCoroutine(FlashRoutine());
    }

    private void StopFlashing()
    {
        _isDead = true;

        if (_flashCoroutine != null)
        {
            StopCoroutine(_flashCoroutine);
            _flashCoroutine = null;
        }

        if (_spriteRenderer != null)
            _spriteRenderer.enabled = true;
    }

    private IEnumerator FlashRoutine()
    {
        float elapsedTime = 0f;

        while (elapsedTime < _duration)
        {
            _spriteRenderer.enabled = !_spriteRenderer.enabled;

            yield return _waitForSeconds;

            elapsedTime += _flashInterval;
        }

        _spriteRenderer.enabled = true;
        _flashCoroutine = null;
    }
}

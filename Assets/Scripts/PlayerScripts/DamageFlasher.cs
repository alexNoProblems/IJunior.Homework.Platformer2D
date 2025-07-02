using UnityEngine;
using System.Collections;

public class DamageFlasher : MonoBehaviour
{
    [SerializeField] private float _duration = 2f;
    [SerializeField] private float _flashInterval = 0.1f;

    private SpriteRenderer _spriteRenderer;
    private Coroutine _flashCoroutine;
    private WaitForSeconds _waitForSeconds;

    private void Awake()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _waitForSeconds = new WaitForSeconds(_flashInterval);
    }

    public void Flash()
    {
        if (_flashCoroutine != null)
            StopCoroutine(_flashCoroutine);

        _flashCoroutine = StartCoroutine(FlashRoutine());
    }

    public void StopFlashing()
    {
        if (_flashCoroutine != null)
        {
            StopCoroutine(_flashCoroutine);
            _spriteRenderer.enabled = true;
            _flashCoroutine = null;
        }
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

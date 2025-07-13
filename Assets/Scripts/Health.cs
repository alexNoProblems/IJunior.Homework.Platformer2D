using System;
using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 5;
    [SerializeField] private int _minHealth = 0;
    [SerializeField] private float _invulnerabilityDuration = 2f;

    private Coroutine _invulnerabilityCoroutine;
    private WaitForSeconds _waitForSeconds;
    private int _currentHealth;
    private bool _isInvulnerable = false;
    private bool _isDead = false;

    public bool IsDead => _isDead;

    public event Action Damaged;
    public event Action Died;

    private void Awake()
    {
        _currentHealth = _maxHealth;
        _waitForSeconds = new WaitForSeconds(_invulnerabilityDuration);
    }

    public void TakeDamage(int damage)
    {
        if (damage < 0 || _isInvulnerable || _isDead)
            return;

        _currentHealth = Mathf.Clamp(_currentHealth - damage, _minHealth, _maxHealth);

        Damaged?.Invoke();

        if (_currentHealth == _minHealth)
        {
            Die();
        }
        else
        {
            _isInvulnerable = true;

            if (_invulnerabilityCoroutine != null)
                StopCoroutine(_invulnerabilityCoroutine);

            _invulnerabilityCoroutine = StartCoroutine(InvulnerabilityRoutine());
        }
    }

    public void Heal(int healValue)
    {
        if (healValue < 0 || _isDead)
            return;

        _currentHealth = Mathf.Clamp(_currentHealth + healValue, _minHealth, _maxHealth);
    }

    public void RestoreFullHealth()
    {
        if (_isDead)
            return;

        _currentHealth = _maxHealth;
    }

    private IEnumerator InvulnerabilityRoutine()
    {
        yield return _waitForSeconds;

        _isInvulnerable = false;
        _invulnerabilityCoroutine = null;
    }

    private void Die()
    {
        _isDead = true;
        Died?.Invoke();
    }
}

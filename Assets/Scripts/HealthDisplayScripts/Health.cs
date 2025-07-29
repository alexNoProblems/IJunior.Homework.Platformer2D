using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 100;
    [SerializeField] private int _minHealth = 0;

    private int _currentHealth;
    private bool _isDead = false;

    public int CurrentHealth => _currentHealth;
    public int MaxHealth => _maxHealth;
    public bool IsDead => _isDead;

    public event Action Damaged;
    public event Action Healed;
    public event Action Died;

    private void Awake()
    {
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (damage < 0 || _isDead)
            return;

        _currentHealth = Mathf.Clamp(_currentHealth - damage, _minHealth, _maxHealth);

        Damaged?.Invoke();

        if (_currentHealth == _minHealth)
            Die();
    }

    public void Heal(int healValue)
    {
        if (healValue < 0 || _isDead)
            return;

        _currentHealth = Mathf.Clamp(_currentHealth + healValue, _minHealth, _maxHealth);

        Healed?.Invoke();
    }

    public void RestoreFullHealth()
    {
        if (_isDead)
            return;

        _currentHealth = _maxHealth;
        Healed?.Invoke();
    }

    private void Die()
    {
        _isDead = true;
        Died?.Invoke();
    }
}

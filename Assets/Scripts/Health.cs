using UnityEngine;

[RequireComponent(typeof(DamageFlasher))]
public class Health : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 5;
    [SerializeField] private int _minHealth = 0;
    [SerializeField] private float _invulnerabilityDuration = 2f;

    private int _currentHealth;
    private bool _isInvulnerable = false;
    private bool _isDead = false;

    private DamageFlasher _flasher;
    private System.Action _onDeath;

    private void Awake()
    {
        _currentHealth = _maxHealth;
        _flasher = GetComponent<DamageFlasher>();
    }

    public void Init(System.Action onDeath)
    {
        _onDeath = onDeath;
    }

    public void TakeDamage(int damage)
    {
        if (_isInvulnerable || _isDead)
            return;

        _currentHealth -= damage;
        _currentHealth = Mathf.Max(_currentHealth, _minHealth);

        if (_flasher != null)
            _flasher.Flash();

        if (_currentHealth == 0)
        {
            Die();
        }
        else
        {
            _isInvulnerable = true;
            Invoke(nameof(ResetInvulnerability), _invulnerabilityDuration);
        }
    }

    public void Heal(int healValue)
    {
        _currentHealth += healValue;
        _currentHealth = Mathf.Min(_currentHealth, _maxHealth);
    }

    private void ResetInvulnerability()
    {
        _isInvulnerable = false;
    }

    private void Die()
    {
        _isDead = true;
        _flasher?.StopFlashing();
        _onDeath?.Invoke();
    }
}

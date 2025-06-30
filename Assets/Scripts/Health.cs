using UnityEngine;

[RequireComponent(typeof(DamageFlasher))]
public class Health : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 5;
    [SerializeField] private int _minHealth = 0;
    [SerializeField] private float _invulnerabilityDuration = 2f;

    private int _currentHealth;
    private bool _isInvulnerable = false;

    private DamageFlasher _flasher;

    private void Awake()
    {
        _currentHealth = _maxHealth;
        _flasher = GetComponent<DamageFlasher>();
    }

    public void TakeDamage(int damage)
    {
        if (_isInvulnerable)
            return;

        _currentHealth -= damage;
        _currentHealth = Mathf.Max(_currentHealth, _minHealth);
        
        Debug.Log(_currentHealth);

        if (_flasher != null)
            _flasher.Flash();

        _isInvulnerable = true;
        Invoke(nameof(ResetInvulnerability), _invulnerabilityDuration);
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
}

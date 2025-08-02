using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class Vampirism : MonoBehaviour
{
    [SerializeField] private float _duration = 6f;
    [SerializeField] private float _cooldown = 4f;
    [SerializeField] private float _flickInterval = 0.5f;
    [SerializeField] private float _range = 5f;
    [SerializeField] private int _damagePerFlick = 1;
    [SerializeField] private int _healPerFlick = 1;

    private bool _isActive = false;
    private bool _isOnCoolDown = false;
    private Health _playerHealth;
    private VampirismEffectUI _effectUI;
    private WaitForSeconds _waitForSecondDuration;
    private WaitForSeconds _waitForSecondCooldown;


    private void Awake()
    {
        _playerHealth = GetComponent<Health>();
        _waitForSecondDuration = new WaitForSeconds(_flickInterval);
        _waitForSecondCooldown = new WaitForSeconds(_cooldown);
    }

    public void Init(VampirismEffectUI effectUI)
    {
        _effectUI = effectUI;
    }

    public void TryActivate()
    {
        if (_isActive || _isOnCoolDown)
            return;

        StartCoroutine(ActivateVampirism());
    }

    private IEnumerator ActivateVampirism()
    {
        _isActive = true;
        _effectUI?.Show(_range, _duration);

        float elapsedTime = 0f;

        while (elapsedTime < _duration)
        {
            elapsedTime += _flickInterval;

            Enemy target = FindClosestEnemyInRange();

            if (target != null && target.TryGetComponent(out Health targetHealth) && !targetHealth.IsDead)
            {
                targetHealth.TakeDamage(_damagePerFlick);
                _playerHealth.Heal(_healPerFlick);
            }

            yield return _waitForSecondDuration;
        }

        _isActive = false;

        StartCoroutine(CoolDownRoutine());
    }

    private IEnumerator CoolDownRoutine()
    {
        _isOnCoolDown = true;
        _effectUI?.ShowCooldown(_cooldown);

        yield return _waitForSecondCooldown;

        _isOnCoolDown = false;
    }

    private Enemy FindClosestEnemyInRange()
    {
        return ClosestEnemyFinder.FindClosestEnemy(transform.position, _range);
    }
}

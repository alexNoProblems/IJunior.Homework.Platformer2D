using System.Collections;
using UnityEngine;
using System;

[RequireComponent(typeof(Health), typeof(ClosestEnemyFinder))]
public class Vampirism : MonoBehaviour
{
    [SerializeField] private float _duration = 6f;
    [SerializeField] private float _cooldown = 4f;
    [SerializeField] private float _flickInterval = 0.5f;
    [SerializeField] private float _range = 5f;
    [SerializeField] private int _damagePerFlick = 1;
    [SerializeField] private int _healPerFlick = 1;

    private Health _playerHealth;
    private ClosestEnemyFinder _enemyFinder;
    private bool _isActive;
    private bool _isOnCoolDown;
    private WaitForSeconds _waitFlick;

    public event Action<float, float> OnActivated;
    public event Action<float> OnCooldownStarted;
    public event Action<float> OnTimerUpdated;
    public event Action<float> OnCooldownUpdated;

    private void Awake()
    {
        _playerHealth = GetComponent<Health>();
        _enemyFinder = GetComponent<ClosestEnemyFinder>();
        _waitFlick = new WaitForSeconds(_flickInterval);
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
        OnActivated?.Invoke(_range, _duration);

        float elapsedTime = 0f;

        while (elapsedTime < _duration)
        {
            Enemy enemy = _enemyFinder.FindClosestEnemy(transform.position, _range);

            if (enemy != null && enemy.TryGetComponent(out Health enemyHealth) && !enemyHealth.IsDead)
            {
                enemyHealth.TakeDamage(_damagePerFlick);
                _playerHealth.Heal(_healPerFlick);
            }

            elapsedTime += _flickInterval;
            OnTimerUpdated?.Invoke(Mathf.Max(0f, _duration - elapsedTime));

            yield return _waitFlick;
        }

        _isActive = false;

        StartCoroutine(CoolDownRoutine());
    }

    private IEnumerator CoolDownRoutine()
    {
        _isOnCoolDown = true;
        OnCooldownStarted?.Invoke(_cooldown);

        float cooldownLeft = _cooldown;

        while (cooldownLeft > 0f)
        {
            OnCooldownUpdated?.Invoke(cooldownLeft);
            cooldownLeft -= Time.deltaTime;

            yield return null;
        }

        _isOnCoolDown = false;
    }
}

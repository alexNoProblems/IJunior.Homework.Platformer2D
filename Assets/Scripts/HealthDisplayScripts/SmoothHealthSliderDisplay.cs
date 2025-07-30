using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SmoothHealthSliderDisplay : HealthDisplayBase
{
    [SerializeField] private Slider _smoothHealthSlider;

    private Coroutine _currentRoutine;
    private float _minSliderValue = 0f;
    private float _maxSliderValue = 1f;

    protected override void Start()
    {
        base.Start();
        _smoothHealthSlider.minValue = _minSliderValue;
        _smoothHealthSlider.maxValue = _maxSliderValue;
        _smoothHealthSlider.value = GetNormalizedHealth();
    }

    protected override void UpdateUI()
    {
        if (_currentRoutine != null)
            StopCoroutine(_currentRoutine);

        float normalizedValue = GetNormalizedHealth();
        _currentRoutine = StartCoroutine(SmoothTransition(normalizedValue));
    }

    private float GetNormalizedHealth()
    {
        if (_health.MaxHealth <= 0)
            return _minSliderValue;

        return (float)_health.CurrentHealth / _health.MaxHealth;
    }

    private IEnumerator SmoothTransition(float targetValue)
    {
        float startValue = _smoothHealthSlider.value;
        float elapsedTime = 0f;
        float durationAnimation = 0.5f;

        while (elapsedTime < durationAnimation)
        {
            elapsedTime += Time.deltaTime;
            float progress = Mathf.Clamp01(elapsedTime / durationAnimation);

            _smoothHealthSlider.value = Mathf.Lerp(startValue, targetValue, progress);

            yield return null;
        }

        _smoothHealthSlider.value = targetValue;
        _currentRoutine = null;
    }
}

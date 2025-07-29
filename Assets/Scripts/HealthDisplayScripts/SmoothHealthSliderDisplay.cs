using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SmoothHealthSliderDisplay : HealthDisplayBase
{
    [SerializeField] private Slider _smoothHealthSlider;
    [SerializeField] private float _smoothSpeed = 50f;

    private Coroutine _currentRoutine;

    protected override void Start()
    {
        base.Start();
        _smoothHealthSlider.maxValue = _health.MaxHealth;
        _smoothHealthSlider.value = _health.CurrentHealth;
    }

    protected override void UpdateUI()
    {
        if (_currentRoutine != null)
            StopCoroutine(_currentRoutine);

        _currentRoutine = StartCoroutine(SmoothTransition(_health.CurrentHealth));
    }

    private IEnumerator SmoothTransition(float targetValue)
    {
        while (Mathf.Abs(_smoothHealthSlider.value - targetValue) > 0.01f)
        {
            _smoothHealthSlider.value = Mathf.MoveTowards(_smoothHealthSlider.value, targetValue, _smoothSpeed * Time.deltaTime);

            yield return null;
        }

        _smoothHealthSlider.value = targetValue;
        _currentRoutine = null;
    }
}

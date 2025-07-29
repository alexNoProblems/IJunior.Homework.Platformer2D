using UnityEngine;
using UnityEngine.UI;

public class HealthSliderDisplay : HealthDisplayBase
{
    [SerializeField] private Slider _healthSlider;

    protected override void Start()
    {
        base.Start();
        _healthSlider.maxValue = _health.MaxHealth;
        UpdateUI();
    }

    protected override void UpdateUI()
    {
        _healthSlider.value = _health.CurrentHealth;
    }
}

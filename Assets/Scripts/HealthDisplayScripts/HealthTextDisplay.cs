using UnityEngine;
using TMPro;

public class HealthTextDisplay : HealthDisplayBase
{
    [SerializeField] private TMP_Text _healthText;

    protected override void UpdateUI()
    {
        _healthText.text = $"{_health.CurrentHealth} / {_health.MaxHealth}";
    }
}

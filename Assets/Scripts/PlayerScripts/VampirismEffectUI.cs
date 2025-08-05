using TMPro;
using UnityEngine;

[RequireComponent(typeof(Vampirism))]
public class VampirismEffectUI : MonoBehaviour
{
    [SerializeField] private VampirismCircle _circle;
    [SerializeField] private VampirismTimerText _timerText;

    private Vampirism _vampirism;

    private void Awake()
    {
        _vampirism = GetComponent<Vampirism>();

        _vampirism.OnActivated += OnActivated;
        _vampirism.OnCooldownStarted += OnCooldownStarted;
    }

    private void OnDestroy()
    {
        if (_vampirism == null)
            return;

        _vampirism.OnActivated -= OnActivated;
        _vampirism.OnCooldownStarted -= OnCooldownStarted;
    }
    private void OnActivated(float radius, float duration)
    {
        _circle.Show(radius);
        _timerText.Show(duration);
    }

    private void OnCooldownStarted(float cooldown)
    {
        _circle.Hide();
        _timerText.ShowCooldown(cooldown);
    }
}

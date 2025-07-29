using UnityEngine;

public abstract class HealthDisplayBase : MonoBehaviour
{
    [SerializeField] protected Health _health;

    protected virtual void OnEnable()
    {
        _health.Damaged += UpdateUI;
        _health.Healed += UpdateUI;
        _health.Died += UpdateUI;
    }

    protected virtual void OnDisable()
    {
        _health.Damaged -= UpdateUI;
        _health.Healed -= UpdateUI;
        _health.Died -= UpdateUI;
    }

    protected virtual void Start()
    {
        UpdateUI();
    }

    protected abstract void UpdateUI();
}

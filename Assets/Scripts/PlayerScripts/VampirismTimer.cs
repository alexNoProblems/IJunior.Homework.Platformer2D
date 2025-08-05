using System;
using UnityEngine;

public class VampirismTimer : MonoBehaviour
{
    private const float CompletionThreshold = 1f;
    private float _duration;
    private float _elapsed;
    private int _lastReportedValue = -1;
    private bool _isCountingDown;
    private bool _isActive;

    public event Action<int> TimeUpdated;
    public event Action Completed;

    public void StartCountdown(float duration)
    {
        _duration = duration;
        _elapsed = 0f;
        _isCountingDown = true;
        _isActive = true;
        _lastReportedValue = -1;
    }

    public void StartRecovery(float duration, float maxDisplayValue)
    {
        _duration = duration;
        _elapsed = 0f;
        _isCountingDown = false;
        _isActive = true;
        _lastReportedValue = -1;
    }

    public void Tick(float deltaTime, float maxDisplayValue)
    {
        if (!_isActive)
            return;

        _elapsed += deltaTime;

        float progress = Mathf.Clamp01(_elapsed / _duration);
        int currentTime;

        if (_isCountingDown)
        {
            currentTime = Mathf.CeilToInt(_duration - _elapsed);
        }
        else
        {
            float interpolatedValue = Mathf.Lerp(0f, maxDisplayValue, progress);
            currentTime = Mathf.FloorToInt(interpolatedValue);
        }

        if (currentTime != _lastReportedValue)
        {
            _lastReportedValue = currentTime;
            TimeUpdated?.Invoke(currentTime);
        }

        if (progress >= CompletionThreshold)
        {
            _isActive = false;
            Completed?.Invoke();
        } 
    }
}

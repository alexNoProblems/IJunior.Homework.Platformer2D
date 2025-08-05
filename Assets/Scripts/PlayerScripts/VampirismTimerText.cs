using TMPro;
using UnityEngine;

[RequireComponent(typeof(VampirismTimer))]
public class VampirismTimerText : MonoBehaviour
{
    [SerializeField] private TMP_Text _timerText;
    [SerializeField] private float _maxDisplayTime = 6f;

    private VampirismTimer _timer;

    private void Awake()
    {
        _timer = GetComponent<VampirismTimer>();
        _timer.TimeUpdated += UpdateText;
        _timer.Completed += Hide;

        gameObject.SetActive(false);
    }

    private void Update()
    {
        _timer?.Tick(Time.deltaTime, _maxDisplayTime);
    }

    private void LateUpdate()
    {
        Vector3 scale = _timerText.rectTransform.localScale;
        scale.x = Mathf.Abs(scale.x);
        _timerText.rectTransform.localScale = scale;

        _timerText.rectTransform.rotation = Quaternion.identity;
    }

    public void Show(float duration)
    {
        gameObject.SetActive(true);
        _timer.StartCountdown(duration);
    }

    public void ShowCooldown(float cooldown)
    {
        gameObject.SetActive(true);
        _timer.StartRecovery(cooldown, _maxDisplayTime);
    }

    public void UpdateText(int seconds)
    {
        _timerText.text = seconds.ToString();
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}

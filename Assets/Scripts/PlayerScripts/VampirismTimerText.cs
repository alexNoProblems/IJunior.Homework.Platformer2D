using TMPro;
using UnityEngine;

public class VampirismTimerText : MonoBehaviour
{
    [SerializeField] private TMP_Text _timerText;

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
        UpdateTime(duration);
    }

    public void ShowCooldown(float cooldown)
    {
        gameObject.SetActive(true);
        UpdateTime(cooldown);
    }

    public void UpdateTime(float seconds)
    {
        _timerText.text = Mathf.CeilToInt(seconds).ToString();
    }
}

using TMPro;
using UnityEngine;

public class VampirismEffectUI : MonoBehaviour
{
    [SerializeField] SpriteRenderer _circleSprite;
    [SerializeField] private TMP_Text _timerText;
    [SerializeField] private float _defaultMaxTime = 6f;

    private float _remainingTime;
    private float _maxTime;
    private float _cooldownDuration;
    private float _minTime = 0f;
    private bool _isActive;
    private bool _isCooldown;
    private bool _isRecovering;

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (!_isActive)
            return;

        if (_isRecovering)
        {
            _remainingTime += Time.deltaTime;

            if (_remainingTime >= _cooldownDuration)
            {
                _isActive = false;
                gameObject.SetActive(false);

                return;
            }

            float mappedTime = Mathf.Lerp(_minTime, _maxTime, _remainingTime / _cooldownDuration);
            _timerText.text = Mathf.CeilToInt(mappedTime).ToString();
        }
        else
        {
            _remainingTime -= Time.deltaTime;

            if (_remainingTime <= 0f)
            {
                _isActive = false;
                gameObject.SetActive(false);

                return;
            }

            int seconds = Mathf.CeilToInt(_remainingTime);
            _timerText.text = seconds.ToString();
        }
    }

    private void LateUpdate()
    {
        Vector3 scale = _timerText.rectTransform.localScale;
        scale.x = Mathf.Abs(scale.x);
        _timerText.rectTransform.localScale = scale;

        _timerText.rectTransform.rotation = Quaternion.identity;
    }

    public void Show(float _radius, float duration)
    {
        float circleSpriteUnits = _circleSprite.sprite.bounds.size.x;
        float circleDiameter = _radius * 2f;
        float targetScale = circleDiameter / circleSpriteUnits;

        _circleSprite.transform.localScale = Vector3.one * targetScale;

        _remainingTime = duration;
        _maxTime = duration;
        _isActive = true;
        _isCooldown = false;
        _isRecovering = false;

        gameObject.SetActive(true);
    }

    public void ShowCooldown(float duration)
    {
        _circleSprite.transform.localScale = Vector3.zero;
        _remainingTime = 0f;
        _maxTime = _defaultMaxTime;
        _cooldownDuration = duration;
        _isActive = true;
        _isCooldown = true;
        _isRecovering = true;

        gameObject.SetActive(true);
    }
}

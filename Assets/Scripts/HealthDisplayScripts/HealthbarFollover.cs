using UnityEngine;

public class HealthbarFollover : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _smoothSpeed = 10f;

    private Camera _mainCamera;
    private RectTransform _rectTransform;

    private void Awake()
    {
        _mainCamera = Camera.main;
        _rectTransform = GetComponent<RectTransform>();
    }

    private void LateUpdate()
    {
        if (_target == null || _mainCamera == null)
            return;


        Vector3 worldPosition = _target.position + _offset;
        Vector2 targetScreenPosition = RectTransformUtility.WorldToScreenPoint(_mainCamera, worldPosition);

        _rectTransform.position = Vector3.Lerp(_rectTransform.position, targetScreenPosition, _smoothSpeed * Time.deltaTime);
    }
}

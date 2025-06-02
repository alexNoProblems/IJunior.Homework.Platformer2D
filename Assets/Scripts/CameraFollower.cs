using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraFollower : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Tilemap _tilemap;
    [SerializeField] private Vector2 _deadZoneSize = new Vector2(0.5f, 0.5f);

    private Vector2 _minBounds;
    private Vector2 _maxBounds;
    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;

        Bounds bounds = _tilemap.localBounds;

        _minBounds = bounds.min;
        _maxBounds = bounds.max;
    }

    private void LateUpdate()
    {
        Vector3 targetPosition = _target.position;

        Vector3 desiredPosition = GetCameraTargetPosition(targetPosition);
        desiredPosition = ClampPositionToBounds(desiredPosition);
        desiredPosition.z = transform.position.z;

        transform.position = desiredPosition;
    }

    private Vector3 GetCameraTargetPosition(Vector3 targetPosition)
    {
        Vector3 currentPosition = transform.position;
        Vector3 newPosition = currentPosition;

        float halfDeadZoneWidth = _deadZoneSize.x * 0.5f;
        float halfDeadZoneHeight = _deadZoneSize.y * 0.5f;

        float leftDeadZone = currentPosition.x - halfDeadZoneWidth;
        float rightDeadZone = currentPosition.x + halfDeadZoneWidth;
        float bottomDeadZone = currentPosition.y - halfDeadZoneHeight;
        float topDeadZone = currentPosition.y + halfDeadZoneHeight;

        if (_target.position.x < leftDeadZone)
            newPosition.x = _target.position.x + halfDeadZoneWidth;
        else if (_target.position.x > rightDeadZone)
            newPosition.x = _target.position.x - halfDeadZoneWidth;

        if (_target.position.y < bottomDeadZone)
            newPosition.y = _target.position.y + halfDeadZoneHeight;
        else if (_target.position.y > topDeadZone)
            newPosition.y = _target.position.y - halfDeadZoneHeight;

        return newPosition;
    }

    private Vector3 ClampPositionToBounds(Vector3 position)
    {
        Vector2 viewSize = new Vector2(_camera.orthographicSize * _camera.aspect, _camera.orthographicSize);

        position.x = Mathf.Clamp(position.x, _minBounds.x + viewSize.x, _maxBounds.x - viewSize.x);
        position.y = Mathf.Clamp(position.y, _minBounds.y + viewSize.y, _maxBounds.y - viewSize.y);

        return position;
    }
}

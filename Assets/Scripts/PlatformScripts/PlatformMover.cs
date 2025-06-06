using UnityEngine;
using System.Collections;

public class PlatformMover : MonoBehaviour
{
    [SerializeField] private float _moveDistance = 2f;
    [SerializeField] private float _moveSpeed = 2f;
    [SerializeField] private float _pauseDuration = 1f;
    [SerializeField] private float _reachThreshold = 0.01f;
    [SerializeField] private bool _isMoveUpInitially = true;

    private Vector3 _startPosition;
    private Vector3 _targetPosition;
    private WaitForSeconds _waitForSeconds;
    private float _sqrReachThreshold;
    private bool _isMovingUp;

    private void Awake()
    {
        _waitForSeconds = new WaitForSeconds(_pauseDuration);
        _sqrReachThreshold = _reachThreshold * _reachThreshold;
    }

    private void Start()
    {
        _startPosition = transform.position;
        _targetPosition = _startPosition + Vector3.up * _moveDistance;
        _isMovingUp = _isMoveUpInitially;

        StartCoroutine(MoveRoutine());
    }

    private IEnumerator MoveRoutine()
    {
        while (true)
        {
            Vector3 startPoint = _isMovingUp ? _startPosition : _targetPosition;
            Vector3 endPoint = _isMovingUp ? _targetPosition : _startPosition;

            while ((transform.position - endPoint).sqrMagnitude > _sqrReachThreshold)
            {
                transform.position = Vector3.MoveTowards(transform.position, endPoint, _moveSpeed * Time.deltaTime);

                yield return null;
            }

            yield return _waitForSeconds;

            _isMovingUp = !_isMovingUp;
        }
    }
}

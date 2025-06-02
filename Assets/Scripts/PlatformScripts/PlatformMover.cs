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
    private bool _isMovingUp;
    private bool _isWaiting;

    private void Awake()
    {
        _waitForSeconds = new WaitForSeconds(_pauseDuration);
    }

    private void Start()
    {
        _startPosition = transform.position;
        _targetPosition = _startPosition + Vector3.up * _moveDistance;
        _isMovingUp = _isMoveUpInitially;
    }

    private void Update()
    {
        if (_isWaiting) return;

        Vector3 target = _isMovingUp ? _targetPosition : _startPosition;
        transform.position = Vector3.MoveTowards(transform.position, target, _moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target) < _reachThreshold)
            StartCoroutine(WaitAndSwitchDirection());
    }

    private IEnumerator WaitAndSwitchDirection()
    {
        _isWaiting = true;

        yield return _waitForSeconds;

        _isMovingUp = !_isMovingUp;
        _isWaiting = false;
    }
}

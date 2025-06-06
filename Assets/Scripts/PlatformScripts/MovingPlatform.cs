
using UnityEngine;

public class MovingPlatform : MonoBehaviour, IMovingPlatform
{
    public Vector2 DeltaMovement { get; private set; }

    private Vector3 _lastPosition;

    private void LateUpdate()
    {
        Vector3 currentPosition = transform.position;
        DeltaMovement = currentPosition - _lastPosition;
        _lastPosition = currentPosition;
    }
}

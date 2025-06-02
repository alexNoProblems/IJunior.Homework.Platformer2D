
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Vector3 DeltaMovement { get; private set; }

    private Vector3 _lastPosition;

    private void LateUpdate()
    {
        DeltaMovement = transform.position - _lastPosition;
        _lastPosition = transform.position;
    }
}

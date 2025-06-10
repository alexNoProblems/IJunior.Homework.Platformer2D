using UnityEngine;

public class SpriteFlipper : MonoBehaviour
{
    public void Flip(float directionX)
    {
        if (Mathf.Approximately(directionX, 0f))
            return;

        Vector3 lockalScale = transform.localScale;
        float desiredX = Mathf.Sign(directionX) * Mathf.Abs(lockalScale.x);

        if (!Mathf.Approximately(lockalScale.x, desiredX))
        {
            lockalScale.x = desiredX;
            transform.localScale = lockalScale;
        }
    }
}

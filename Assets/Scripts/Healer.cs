using UnityEngine;

public class Healer : MonoBehaviour
{
    public void ApplyHealingTo(Health health)
    {
        if (health != null)
            health.RestoreFullHealth();
    }
}

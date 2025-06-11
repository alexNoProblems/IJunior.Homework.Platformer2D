using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Coin : MonoBehaviour
{
    public bool CanSpawnOnCollect { get; private set; } = true;

    public void Init(bool canSpawn)
    {
        CanSpawnOnCollect = canSpawn;
    }
}

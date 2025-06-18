using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Coin : MonoBehaviour
{
    public bool CanSpawnOnCollect { get; private set; } = true;

    public event Action<Coin> OnCollected;

    public void Init(bool canSpawn)
    {
        CanSpawnOnCollect = canSpawn;
    }

    public void Collect()
    {
        OnCollected?.Invoke(this);
    }

    public void ClearListeners()
    {
        OnCollected = null;
    }
}

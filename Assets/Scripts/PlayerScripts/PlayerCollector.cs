using System;
using UnityEngine;

public class PlayerCollector : MonoBehaviour, ITriggerHandler2D
{
    public static event Action<Coin> OnCoinCollected;
    [SerializeField, Tooltip("Coin collect Sound")] private AudioSource _coinCollectSound;

    private int _coinCount = 0;

    public void HandleTriggerEnter2D(Collider2D other)
    {
        Coin coin = other.GetComponent<Coin>();

        if (coin != null)
        {
            _coinCount++;
            Debug.Log("Количество монет: " + _coinCount);

            if (_coinCollectSound != null)
                _coinCollectSound.Play();

            OnCoinCollected?.Invoke(coin);
        }
    }
}

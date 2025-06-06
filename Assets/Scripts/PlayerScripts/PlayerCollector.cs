using UnityEngine;

public class PlayerCollector : MonoBehaviour, ITriggerHandler2D
{
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

            Destroy(coin.gameObject);
        }
    }
}
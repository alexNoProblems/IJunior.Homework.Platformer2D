using UnityEngine;

public class PlayerCollector : MonoBehaviour, ITriggerHandler2D
{
    private ItemsCollectSoundPlayer _soundPlayer;
    private int _coinCount = 0;

    public void HandleTriggerEnter2D(Collider2D other)
    {
        Coin coin = other.GetComponent<Coin>();

        if (coin != null)
        {
            _coinCount++;
            Debug.Log("Количество монет: " + _coinCount);

            _soundPlayer?.PlaySound();
            coin.Collect();
        }
    }

    public void Init(ItemsCollectSoundPlayer soundPlayer)
    {
        _soundPlayer = soundPlayer;
    }
}

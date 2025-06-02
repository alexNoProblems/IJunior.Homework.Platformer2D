using UnityEngine;

public class PlayerCollector : MonoBehaviour
{
    private int _coinCount = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Coin coin = collision.GetComponent<Coin>();

        if (coin != null)
        {
            _coinCount++;
            Debug.Log("Количество монет: " + _coinCount);
        }
    }
}

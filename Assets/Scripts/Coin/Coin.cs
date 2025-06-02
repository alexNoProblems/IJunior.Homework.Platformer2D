using UnityEngine;

[RequireComponent(typeof(CoinCollectPlayer))]
public class Coin : MonoBehaviour
{
    public void Collect()
    {
        CoinCollectPlayer soundPlayer = GetComponent<CoinCollectPlayer>();

        if (soundPlayer != null)
            soundPlayer.PlaySound();

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerCollector collector = collision.GetComponent<PlayerCollector>();

        if (collector != null)
        {
            CoinSpawner spawner = GetComponent<CoinSpawner>();

            if (spawner != null)
                spawner.Spawn();

            Collect();
        }
    }
}

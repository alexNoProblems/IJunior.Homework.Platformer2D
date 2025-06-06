using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private Coin _coinPrefab;
    [SerializeField] private Vector2 _spawnOffset = new Vector2(1f, 0f);
    [SerializeField] private int _maxSpawns = 3;
    [SerializeField] private int _spawnPerTrigger = 1;

    private readonly List<Coin> _coinPool = new List<Coin>();
    private int _spawnCount = 0;

    private void OnEnable()
    {
        PlayerCollector.OnCoinCollected += HandleCoinCollected;
        FillPool();
    }

    private void OnDisable()
    {
        PlayerCollector.OnCoinCollected -= HandleCoinCollected;
    }

    public void Spawn()
    {
        for (int i = 0; i < _spawnPerTrigger; i++)
        {
            Coin coin = GetCoinFromPool();

            if (coin == null)
                return;

            Vector2 spawnPosition = (Vector2)transform.position + _spawnOffset;
            coin.transform.position = spawnPosition;
            coin.gameObject.SetActive(true);

            _spawnCount++;
        }
    }

    private void FillPool()
    {
        _coinPool.Clear();

        for (int i = 0; i < _maxSpawns; i++)
        {
            Coin coin = Instantiate(_coinPrefab, transform.position, Quaternion.identity);
            coin.gameObject.SetActive(false);
            _coinPool.Add(coin);
        }
    }

    private Coin GetCoinFromPool()
    {
        foreach (var coin in _coinPool)
        {
            if (!coin.gameObject.activeInHierarchy)
                return coin;
        }

        return null;
    }

    private void HandleCoinCollected(Coin coin)
    {
        coin.gameObject.SetActive(false);
        _spawnCount--;

        Spawn();
    }
}

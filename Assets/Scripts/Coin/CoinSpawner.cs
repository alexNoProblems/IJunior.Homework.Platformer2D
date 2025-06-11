using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private Coin _coinPrefab;
    [SerializeField] private float _minSpawnRadius = 2f;
    [SerializeField] private float _maxSpawnRadius = 5f;
    [SerializeField] private float _maxCoinNumber = 20;

    private readonly List<Coin> _coinPool = new List<Coin>();

    private void OnEnable()
    {
        PlayerCollector.OnCoinCollected += HandleCoinCollected;
        FillPool();
    }

    private void OnDisable()
    {
        PlayerCollector.OnCoinCollected -= HandleCoinCollected;
    }

    private void FillPool()
    {
        _coinPool.Clear();

        for (int i = 0; i < _maxCoinNumber; i++)
        {
            Coin coin = Instantiate(_coinPrefab, transform.position, Quaternion.identity);
            coin.gameObject.SetActive(false);
            coin.Init(true);
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

    private void HandleCoinCollected(Coin collectedCoin)
    {
        collectedCoin.gameObject.SetActive(false);

        if (collectedCoin.CanSpawnOnCollect)
            SpawnRandomCoin(collectedCoin.transform.position, _minSpawnRadius, _maxSpawnRadius);
    }

    private void SpawnRandomCoin(Vector2 center, float minRadius, float maxRadius)
    {
        Vector2 randomOffset = Random.insideUnitCircle.normalized * Random.Range(_minSpawnRadius, _maxSpawnRadius);
        Vector2 spawnPosition = center + randomOffset;

        Coin coin = GetCoinFromPool();

        if (coin == null)
            return;

        coin.transform.position = spawnPosition;
        coin.gameObject.SetActive(true);
        coin.Init(false);
    }
}

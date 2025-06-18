using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private Coin _coinPrefab;
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private int _coinPoolSize = 16;
    [SerializeField] private int _initialActiveCoins = 5;

    private readonly List<Coin> _coinPool = new List<Coin>();

    private int _nextSpawnIndex = 0;

    private void OnEnable()
    {
        if (_coinPool.Count == 0)
            FillPool();
        
        SpawnInitialCoins();
    }

    private void OnDisable()
    {
        foreach (var coin in _coinPool)
            coin.ClearListeners();
    }

    private void FillPool()
    {
        for (int i = 0; i < _coinPoolSize; i++)
        {
            Coin coin = Instantiate(_coinPrefab, transform.position, Quaternion.identity);
            coin.gameObject.SetActive(false);
            _coinPool.Add(coin);
        }
    }

    private void SpawnInitialCoins()
    {
        _nextSpawnIndex = 0;

        for (int i = 0; i < _initialActiveCoins && i < _spawnPoints.Length; i++)
        {
            SpawnCoinAt(_spawnPoints[i].position);
            _nextSpawnIndex++;
        }
    }

    private void SpawnCoinAt(Vector2 position)
    {
        Coin coin = GetCoinFromPool();

        if (coin == null)
            return;

        coin.transform.position = position;
        coin.gameObject.SetActive(true);

        coin.ClearListeners();
        coin.OnCollected += HandleCoinCollected;
        coin.Init(true);
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
        coin.OnCollected -= HandleCoinCollected;
        coin.gameObject.SetActive(false);

        if (_nextSpawnIndex < _spawnPoints.Length)
        {
            SpawnCoinAt(_spawnPoints[_nextSpawnIndex].position);
            _nextSpawnIndex++;
        }
    }
}

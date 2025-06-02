using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private Coin _coinPrefab;
    [SerializeField] private Vector2 _spawnOffset = new Vector2(1f, 0f);
    [SerializeField] private int _maxSpawns = 3;
    [SerializeField] private int _spawnPerTrigger = 1;

    private int _spawnCount = 0;

    public void Spawn()
    {
        if (_coinPrefab == null)
            return;

        for (int i = 0; i < _spawnPerTrigger; i++)
        {
            if (_spawnCount >= _maxSpawns)
                return;
            
            Vector2 spawnPosition = (Vector2)transform.position + _spawnOffset;
            Instantiate(_coinPrefab, spawnPosition, Quaternion.identity);
            _spawnCount++;
        }
    }
}

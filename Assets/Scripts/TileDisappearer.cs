using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;

[RequireComponent(typeof(Tilemap))]
public class TileDisappearer : MonoBehaviour
{
    [SerializeField] private float _delayBeforeDisappearer = 0.1f;

    private Tilemap _tileMap;
    private WaitForSeconds _waitForSeconds;

    private void Awake()
    {
        _tileMap = GetComponent<Tilemap>();
        _waitForSeconds = new WaitForSeconds(_delayBeforeDisappearer);
    }

    public void OnEnterCollision2D(Collision2D collision)
    {
        if (!collision.collider.TryGetComponent<Player>(out _))
            return;

        foreach (ContactPoint2D contact in collision.contacts)
        {
            Vector3 hitPoint = contact.point;
            Vector3Int cellPosition = _tileMap.WorldToCell(hitPoint);
            StartCoroutine(RemoveTileWithDelay(cellPosition));
        }
    }
    private IEnumerator RemoveTileWithDelay(Vector3Int cellPosition)
    {
        yield return _waitForSeconds;

        _tileMap.SetTile(cellPosition, null);
    }
}
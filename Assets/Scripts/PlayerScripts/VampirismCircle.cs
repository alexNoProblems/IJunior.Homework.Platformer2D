using UnityEngine;

public class VampirismCircle : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _sprite;

    public void Show(float radius)
    {
        float diametr = radius * 2f;
        float units = _sprite.sprite.bounds.size.x;
        float scale = diametr / units;

        _sprite.transform.localScale = Vector3.one * scale;
        _sprite.enabled = true;
    }

    public void Hide()
    {
        _sprite.enabled = false;
    }
}

using UnityEngine;

public class VampirismCircle : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _sprite;

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    public void Show(float radius)
    {
        gameObject.SetActive(true);
        _sprite.enabled = true;

        float diameter = radius * 2f;
        float units = _sprite.sprite.bounds.size.x;

        float scale = diameter / units;
        _sprite.transform.localScale = Vector3.one * scale;
    }

    public void Hide()
    {
        _sprite.enabled = false;
    }
}

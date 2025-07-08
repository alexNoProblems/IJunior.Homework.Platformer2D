using UnityEngine;

[RequireComponent(typeof(Health))]
public class PlayerHealerHandler : MonoBehaviour, ITriggerHandler2D
{
    private Health _health;
    private ItemsCollectSoundPlayer _soundPlayer;

    private void Awake()
    {
        _health = GetComponent<Health>();
    }

    public void HandleTriggerEnter2D(Collider2D other)
    {
        Healer healer = other.GetComponent<Healer>();

        if (healer != null && _health != null)
        {
            healer.ApplyHealingTo(_health);
            _soundPlayer?.PlaySound();
            Destroy(other.gameObject);
        }
    }

    public void Init(ItemsCollectSoundPlayer soundPlayer)
    {
        _soundPlayer = soundPlayer;
    }
}

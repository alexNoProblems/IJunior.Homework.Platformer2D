using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class DeathSoundPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip _deathSound;

    private AudioSource _audioSource;
    private Health _health;

    public void Init(Health health)
    {
        _health = health;
        _audioSource = GetComponent<AudioSource>();

        health.Died += PlayDeathSound;
    }

    private void OnDestroy()
    {
        if (_health != null)
            _health.Died -= PlayDeathSound;
    }

    private void PlayDeathSound()
    {
        if (_deathSound != null)
            _audioSource.PlayOneShot(_deathSound);
    }
}

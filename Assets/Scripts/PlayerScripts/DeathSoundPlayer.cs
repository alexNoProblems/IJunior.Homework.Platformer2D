using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class DeathSoundPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip _deathSound;
    private AudioSource _audioSource;

    public void Init(PlayerKiller playerKiller)
    {
        _audioSource = GetComponent<AudioSource>();

        playerKiller.OnDie += PlayDeathSound;
    }

    private void PlayDeathSound()
    {
        if (_deathSound != null)
            _audioSource.PlayOneShot(_deathSound);
    }
}

using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class DeathSoundPlayer : MonoBehaviour
{
    private AudioClip _deathSound;
    private AudioSource _audioSource;

    public void Init(PlayerKiller playerKiller, AudioClip deathSound)
    {
        _deathSound = deathSound;
        _audioSource = GetComponent<AudioSource>();

        playerKiller.OnDie += PlayDeathSound;
    }

    private void PlayDeathSound()
    {
        if (_deathSound != null)
            _audioSource.PlayOneShot(_deathSound);
    }
}

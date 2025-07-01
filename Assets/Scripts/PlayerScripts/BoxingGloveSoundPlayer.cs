using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BoxingGloveSoundPlayer : MonoBehaviour
{
    private AudioClip _boxingGloveKickSound;
    private AudioSource _audioSource;

    public void Init(Puncher puncher, AudioClip boxingGloveKickSound)
    {
        _boxingGloveKickSound = boxingGloveKickSound;
        _audioSource = GetComponent<AudioSource>();

        puncher.OnKick += PlayKickSound;
    }

    private void PlayKickSound()
    {
        if (_boxingGloveKickSound != null)
            _audioSource.PlayOneShot(_boxingGloveKickSound);
    }
}

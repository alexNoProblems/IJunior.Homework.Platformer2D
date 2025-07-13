using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BoxingGloveSoundPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip _boxingGloveKickSound;

    private AudioSource _audioSource;
    private Puncher _puncher;

    public void Init(Puncher puncher)
    {
        _audioSource = GetComponent<AudioSource>();

        puncher.Punched += PlayKickSound;
    }

    private void OnDestroy()
    {
        if (_puncher != null)
            _puncher.Punched -= PlayKickSound;
    }

    private void PlayKickSound()
    {
        if (_boxingGloveKickSound != null)
            _audioSource.PlayOneShot(_boxingGloveKickSound);
    }
}

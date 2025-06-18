using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class CoinCollectSoundPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip _collectSound;

    private float maxCoinVolume = 1f;

    public void PlaySound()
    {
        if (_collectSound != null)
            AudioSource.PlayClipAtPoint(_collectSound, transform.position, maxCoinVolume);
    }
}

using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class JumpSoundPlayer : MonoBehaviour
{
    private AudioClip _jumpSound;
    private AudioSource _audioSource;

    public void Init(PlayerJumper jumper, AudioClip jumpSound)
    {
        _jumpSound = jumpSound;
        _audioSource = GetComponent<AudioSource>();

        jumper.OnJumped += PlayJumpSound;
    }

    private void PlayJumpSound()
    {
        if (_jumpSound != null)
            _audioSource.PlayOneShot(_jumpSound);
    }
}

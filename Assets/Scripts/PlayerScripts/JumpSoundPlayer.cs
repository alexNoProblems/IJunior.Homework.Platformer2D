using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class JumpSoundPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip _jumpSound;

    private AudioSource _audioSource;
    private PlayerJumper _jumper;

    public void Init(PlayerJumper jumper)
    {
        _jumper = jumper;
        _audioSource = GetComponent<AudioSource>();

        jumper.Jumped += PlayJumpSound;
    }

    private void OnDestroy()
    {
        if (_jumper != null)
            _jumper.Jumped -= PlayJumpSound;
    }

    private void PlayJumpSound()
    {
        if (_jumpSound != null)
            _audioSource.PlayOneShot(_jumpSound);
    }
}

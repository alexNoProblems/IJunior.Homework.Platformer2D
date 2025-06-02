using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class JumpSoundPlayer : MonoBehaviour
{
    [SerializeField] private PlayerJumper _playerJumper;
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private AudioClip _jumpSound;

    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (_inputReader.IsJumpPressed && _playerJumper.IsGrounded())
            PlayJumpSound();
    }

    private void PlayJumpSound()
    {
        if (_jumpSound != null)
            _audioSource.PlayOneShot(_jumpSound);
    }
}

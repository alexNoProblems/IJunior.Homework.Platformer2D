using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class JumpSoundPlayer : MonoBehaviour
{
    private PlayerJumper _playerJumper;
    private InputReader _inputReader;
    private AudioClip _jumpSound;
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

    public void Init(PlayerJumper jumper, InputReader inputReader, AudioClip jumpSound)
    {
        _playerJumper = jumper;
        _inputReader = inputReader;
        _jumpSound = jumpSound;
    }

    private void PlayJumpSound()
    {
        if (_jumpSound != null)
            _audioSource.PlayOneShot(_jumpSound);
    }
}

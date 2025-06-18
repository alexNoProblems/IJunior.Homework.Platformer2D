using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(PlayerMover), typeof(PlayerJumper), typeof(JumpSoundPlayer))]
[RequireComponent(typeof(PlayerCollector), typeof(SpriteFlipper))]
public class Player : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private Transform _groundChecker;
    [SerializeField] private PlayerAnimator _playerAnimator;
    [SerializeField] private AudioClip _jumpSound;
    [SerializeField] private CoinCollectSoundPlayer _coinSoundPlayer;

    private PlayerMover _mover;
    private PlayerJumper _jumper;
    private PlayerCollector _collector;
    private JumpSoundPlayer _jumpSoundPlayer;
    private SpriteFlipper _spriteFlipper;

    private void Awake()
    {
        _mover = GetComponent<PlayerMover>();
        _jumper = GetComponent<PlayerJumper>();
        _collector = GetComponent<PlayerCollector>();
        _jumpSoundPlayer = GetComponent<JumpSoundPlayer>();
        _spriteFlipper = GetComponent<SpriteFlipper>();

        _mover.Init(_inputReader);
        _jumper.Init(_inputReader, _groundChecker);
        _jumpSoundPlayer.Init(_jumper, _inputReader, _jumpSound);
        _playerAnimator.Init(_inputReader, _jumper, _spriteFlipper);
        _collector.Init(_coinSoundPlayer);
    }
}

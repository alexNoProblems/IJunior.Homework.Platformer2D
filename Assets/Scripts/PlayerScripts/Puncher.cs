using UnityEngine;
using System;

[RequireComponent(typeof(Animator))]
public class Puncher : MonoBehaviour
{
    private static readonly int _punchRightHash = Animator.StringToHash("PunchRight");
    private static readonly int _punchLeftHash = Animator.StringToHash("PunchLeft");

    public event Action OnKick;

    private Glove _glove;
    private Animator _animator;
    private Transform _gloveSpawnPoint;
    private GloveEnemyKiller _gloveEnemyKiller;

    public void Init(Glove glove, Transform gloveSpawnPoint)
    {
        _glove = glove;
        _gloveSpawnPoint = gloveSpawnPoint;
        _animator = glove.GetComponent<Animator>();
        _gloveEnemyKiller = glove.GetComponent<GloveEnemyKiller>();

        _glove.gameObject.SetActive(false);
    }

    public void TryPunch(float facingDirection)
    {
        if (_glove == null || _animator == null || _gloveSpawnPoint == null)
            return;

        if (_gloveEnemyKiller != null)
        {
            Vector2 punchDirection = new Vector2(Mathf.Sign(facingDirection), 0f);
            _gloveEnemyKiller.PrepareForAttack(punchDirection);
        }
        
        _glove.transform.position = _gloveSpawnPoint.position;
        _glove.transform.rotation = _gloveSpawnPoint.rotation;

        _glove.gameObject.SetActive(true);

        _animator.ResetTrigger(_punchLeftHash);
        _animator.ResetTrigger(_punchRightHash);

        if (facingDirection > 0)
            _animator.SetTrigger(_punchRightHash);
        else
            _animator.SetTrigger(_punchLeftHash);

        OnKick?.Invoke();
    }

    public void HideGlove()
    {
        if (_glove != null)
            _glove.gameObject.SetActive(false);
    }
}

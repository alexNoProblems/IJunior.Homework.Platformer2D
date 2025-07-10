using UnityEngine;
using System;

[RequireComponent(typeof(BoxingGloveAnimator))]
public class Puncher : MonoBehaviour
{
    public event Action OnKick;

    private Glove _glove;
    private BoxingGloveAnimator _gloveAnimator;
    private Transform _gloveSpawnPoint;
    private GloveEnemyKiller _gloveEnemyKiller;

    public void Init(Glove glove, Transform gloveSpawnPoint)
    {
        _glove = glove;
        _gloveSpawnPoint = gloveSpawnPoint;
        _gloveAnimator = glove.GetComponent<BoxingGloveAnimator>();
        _gloveEnemyKiller = glove.GetComponent<GloveEnemyKiller>();

        _glove.gameObject.SetActive(false);
    }

    public void TryPunch(float facingDirection)
    {
        if (_glove == null || _gloveAnimator == null || _gloveSpawnPoint == null)
            return;

        if (_gloveEnemyKiller != null)
        {
            Vector2 punchDirection = new Vector2(Mathf.Sign(facingDirection), 0f);
            _gloveEnemyKiller.PrepareForAttack(punchDirection);
        }
        
        _glove.transform.position = _gloveSpawnPoint.position;
        _glove.transform.rotation = _gloveSpawnPoint.rotation;

        _glove.gameObject.SetActive(true);
        _gloveAnimator.PlayPunch(facingDirection);

        OnKick?.Invoke();
    }

    public void HideGlove()
    {
        if (_glove != null)
            _glove.gameObject.SetActive(false);
    }
}
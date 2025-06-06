using UnityEngine;

public interface ICollisionHandler2D
{
    void OnEnterCollision2D(Collision2D collision);
    void OnStayCollision2D(Collision2D collision);
    void OnExitCollision2D(Collision2D collision);
}

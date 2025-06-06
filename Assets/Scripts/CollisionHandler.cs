using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class CollisionHandler : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        var triggerHandlers = GetComponents<ITriggerHandler2D>();

        foreach (var handler in triggerHandlers)
            handler.HandleTriggerEnter2D(other);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var collisionHandlers = GetComponents<ICollisionHandler2D>();

        foreach (var handler in collisionHandlers)
            handler.OnEnterCollision2D(collision);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        var collisionHandlers = GetComponents<ICollisionHandler2D>();

        foreach (var handler in collisionHandlers)
            handler.OnStayCollision2D(collision);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        var collisionHandlers = GetComponents<ICollisionHandler2D>();

        foreach (var handler in collisionHandlers)
            handler.OnExitCollision2D(collision);
    }
}

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
        var tileHandlers = GetComponents<TileDisappearer>();

        foreach (var handler in tileHandlers)
            handler.OnEnterCollision2D(collision);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBounce : MonoBehaviour
{
    public float bounceForce = 10.0f;

    void OnCollisionEnter2D(Collision2D collision)
    {
        BounceOffWall(collision.contacts[0].normal);
    }

    void BounceOffWall(Vector2 collisionNormal)
    {
        Vector2 bounceDirection = Vector2.Reflect(transform.up, collisionNormal);

        GetComponent<Rigidbody2D>().velocity = bounceDirection * bounceForce;
    }
}

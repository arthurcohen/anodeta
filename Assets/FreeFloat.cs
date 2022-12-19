using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeFloat : MonoBehaviour
{
    public bool contained = true;
    public float mass = 10f;
    public float drag = 1f;
    private bool killOnExit = false;
    private Vector2 lastVelocity = Vector2.zero;
    private Rigidbody2D rb;
    void Start()
    {
        killOnExit = !contained;

        rb = gameObject.GetComponent<Rigidbody2D>();

        if (rb == null) {
            rb = gameObject.AddComponent<Rigidbody2D>();
        }

        rb.mass = mass;
        rb.drag = drag;
    }

    // Update is called once per frame
    void Update()
    {
        lastVelocity = rb.velocity;
    }

    private void FixedUpdate() {
        if (contained) {
            CollisionHelper.detectWallCollisions(gameObject);
        } else {
            if (CollisionHelper.isOutOfBounds(gameObject)) {
                try {
                    gameObject.GetComponent<GenericBrick>().kill();
                } catch (System.Exception) {
                    throw;
                }
            }
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 lastVelocity = Vector2.zero;
    private bool isMoving = false;
    private GenericPowerController powerController;
    private GenericGameController gameController;
    private Vector2 mapBoundaries;
    public float maxSpeed = 3f;
    public float speedModifier = 1f;
    public float initialBallHeight = -3.5f;
    public Vector2 gravityPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(3, 3);
        powerController = GameObject.FindObjectOfType<GenericPowerController>();
        gameController = GameObject.FindObjectOfType<GenericGameController>();
        mapBoundaries = gameController.detectMapBoundaries();
    }

    // Update is called once per frame
    void FixedUpdate() {
        modulateSpeed();
        rb.velocity = isMoving ? rb.velocity : Vector2.zero;
        lastVelocity = rb.velocity;
        detectWallCollisions();

    }

    void Update() {
        if (!isMoving) followPaddle();
    }

    private void detectWallCollisions() {
        float ballRadius = transform.lossyScale.y / 2f;

        if (rb.position.x >= mapBoundaries.x - ballRadius && rb.velocity.x > 0) genericCollisionResolver(Vector2.left);
        if (rb.position.x <= -mapBoundaries.x + ballRadius && rb.velocity.x < 0) genericCollisionResolver(Vector2.right);
        if (rb.position.y >= mapBoundaries.y - ballRadius && rb.velocity.y > 0) genericCollisionResolver(Vector2.up);
        if (rb.position.y <= -mapBoundaries.y - ballRadius && rb.velocity.y < 0) kill();
    }

    private void modulateSpeed() {
        // TODO fix gravitational force on player click
        if (gravityPosition != null && false) {
            // rb.AddForce(Quaternion.AngleAxis(90, Vector3.forward) * rb.velocity);

            float angleToGravity = Vector2.SignedAngle(rb.velocity, gravityPosition - rb.position);

            Debug.Log(angleToGravity);
            angleToGravity = UnityEngine.Mathf.Clamp(angleToGravity, -90, 90);
            Debug.Log(angleToGravity);

            Vector2 gravityForce = Quaternion.Euler(0, 0, angleToGravity) * rb.velocity.normalized * angleToGravity / 10;
            Debug.DrawLine(rb.position, rb.position + gravityForce, Color.yellow);

            rb.AddForce(gravityForce);

            Debug.DrawLine(rb.position, gravityPosition, Color.black);
        }

        rb.velocity = rb.velocity.normalized * UnityEngine.Mathf.Lerp(rb.velocity.magnitude, maxSpeed * speedModifier, 0.3f * Time.deltaTime);
    }

    private void playerCollisionHandler(Collision2D collision) {
        if (collision.gameObject.tag != "Player") return;

        Vector2 newDirection = (transform.position - collision.transform.position).normalized * lastVelocity.magnitude;
            
        rb.velocity = newDirection;
        Debug.DrawLine(collision.transform.position, transform.position, Color.grey, 3f);
    } 

    private void genericCollisionHandler(Collision2D collision) {
        Vector2 lastNormal = Vector2.zero;

        for (int i = 0; i < collision.contactCount; i++) {
            ContactPoint2D contact = collision.GetContact(i);
            Debug.DrawLine(transform.position, rb.position + contact.normal, Color.yellow, 3f);
            Debug.DrawLine(transform.position, rb.position + rb.velocity.normalized, Color.red, 3f);
            Debug.DrawLine(transform.position, rb.position + lastVelocity.normalized, Color.blue, 3f);

            lastNormal = contact.normal;
        }

        // rb.velocity = Vector2.Reflect(lastVelocity, lastNormal);
        genericCollisionResolver(lastNormal);
    }

    private void genericCollisionResolver(Vector2 normal) {
        rb.velocity = Vector2.Reflect(lastVelocity, normal);
    }

    void OnCollisionEnter2D(Collision2D collision) {
        genericCollisionHandler(collision);
        playerCollisionHandler(collision);
        genericBrickCollisionHandler(collision);
    }

    private void followPaddle() {
        Vector2 paddlePosition = GameObject.FindObjectOfType<Paddle>().transform.position;
        transform.position = new Vector2(paddlePosition.x, paddlePosition.y + transform.lossyScale.y);
    }

    private void kill() {
        Debug.Log("dead");
        gameController.loseHealth();
        Destroy(gameObject);
;    }

    private void genericBrickCollisionHandler(Collision2D collision) {
        GenericBrick gb = collision.gameObject.GetComponent<GenericBrick>();

        if (gb != null) {
            gb.dealDamage(1);
            if (powerController.canSpawnPower()) powerController.spawnPower(collision.transform.position);
        }
    }

    public void randomColor() {
        UnityEngine.Random.Range(0f, 1f);
        GetComponent<SpriteRenderer>().color = new Color(
            UnityEngine.Random.Range(0f, 1f),
            UnityEngine.Random.Range(0f, 1f),
            UnityEngine.Random.Range(0f, 1f), 
            0.5f
        );
    }

    public void launch() {
        if (isMoving) return;

        isMoving = true;
        rb.velocity = Vector2.up * maxSpeed;
    }

    public void resetBall(bool isMoving) {
        this.isMoving = isMoving;
        speedModifier = 1f;
        transform.position = new Vector2(0, initialBallHeight);
    }
}

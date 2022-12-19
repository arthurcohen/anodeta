using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    private Rigidbody2D rb;
    public float maxSpeed = 5f;
    private float speed;
    private Vector2 lastExpectedPosition = Vector2.zero;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        speed = maxSpeed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        movePaddle();
    }

    public void move(Vector2 controllerPosition) {
        lastExpectedPosition = controllerPosition;
    }

    void movePaddle() {
        lastExpectedPosition.y = transform.position.y;
        Vector2 newPosition = Vector2.Lerp(transform.position, lastExpectedPosition, speed * Time.deltaTime);

        Debug.DrawLine(transform.position, lastExpectedPosition, Color.red);
        Debug.DrawLine(Vector2.zero, transform.position, Color.magenta);
        Debug.DrawLine(Vector2.zero, lastExpectedPosition, Color.blue);

        // transform.position = (newPosition);
        rb.MovePosition(newPosition);
    }

    void OnCollisionEnter2D(Collision2D hit) {
        // rb.velocity = Vector2.zero;
    }

    public void setSpeed(float newSpeed) {
        speed = newSpeed;
    }

    public void resetSpeed() {
        speed = maxSpeed;
    }
}

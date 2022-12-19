using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericBrick : MonoBehaviour
{
    public int initialHealthPoints = 1;
    private int healthPoints;
    private Vector2 initialPosition;
    private bool isDead = false;

    void Start() {
        healthPoints = initialHealthPoints;
        initialPosition = transform.position;
    }

    void Update() {
        checkDeath();
    }

    void checkDeath() {
        if (!isDead && healthPoints <= 0) {
            kill();
        }
    }

    public void kill() {
        GetComponent<Collider2D>().enabled = false;
        GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
        isDead = true;
    }

    public void dealDamage(int damageAmount) {
        healthPoints = healthPoints - damageAmount;
    }

    public void resetBrick() {
        healthPoints = initialHealthPoints;
        transform.position = initialPosition;
        isDead = false;

        try {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0;
            rb.rotation = 0;
        } catch (System.Exception) {}

        GetComponent<Collider2D>().enabled = true;
        GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
    }

    public int getHealthPoints() {
        return healthPoints;
    }
}

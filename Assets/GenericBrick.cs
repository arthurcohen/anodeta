using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericBrick : MonoBehaviour
{
    public int initialHealthPoints = 1;
    public int healthPoints;
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
            GetComponent<Collider2D>().enabled = false;
            GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
            isDead = true;
        }
    }

    public void dealDamage(int damageAmount) {
        healthPoints = healthPoints - damageAmount;
    }

    public void resetBrick() {
        healthPoints = initialHealthPoints;
        transform.position = initialPosition;
        GetComponent<Collider2D>().enabled = true;
        GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
    }
}

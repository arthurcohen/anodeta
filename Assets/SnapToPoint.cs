using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapToPoint : MonoBehaviour
{
    public Transform targetObject;
    private Vector3 target;
    public float snapForce = 5f;
    private Vector3 velocity = Vector3.zero;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.AddComponent<Rigidbody2D>();

        target = transform.position;
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (targetObject != null) {
            target = targetObject.position;
        }


        Vector2 snappingDirection = target - transform.position;

        Debug.DrawLine(transform.position, target, Color.red);
        Debug.DrawLine(transform.position, rb.position + snappingDirection.normalized, Color.blue);
        Debug.DrawLine(transform.position, rb.position + rb.velocity, Color.green);

        if (snappingDirection.magnitude > 0.1f) {
            rb.AddForce(snappingDirection.normalized * snapForce);
            rb.AddForce(-rb.velocity * 1 / snappingDirection.magnitude * snapForce);
        }
    }
}

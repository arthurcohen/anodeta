using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static class CollisionHelper
{
  public static void detectWallCollisions(GameObject go, Action downCallback = null, Action upCallback = null, Action leftCallback = null, Action rightCallback = null)
  {
    float ballRadius = go.transform.lossyScale.y / 2f;
    Rigidbody2D rb = go.GetComponent<Rigidbody2D>();
    Vector3 mapBoundaries = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

    if (rb.position.x >= mapBoundaries.x - ballRadius && rb.velocity.x > 0)
    {
      genericCollisionResolver(rb, Vector2.left, rb.velocity);
      if (leftCallback != null) leftCallback();
    }
    if (rb.position.x <= -mapBoundaries.x + ballRadius && rb.velocity.x < 0)
    {
      genericCollisionResolver(rb, Vector2.right, rb.velocity);
      if (rightCallback != null) rightCallback();
    }
    if (rb.position.y >= mapBoundaries.y - ballRadius && rb.velocity.y > 0)
    {
      genericCollisionResolver(rb, Vector2.up, rb.velocity);
      if (upCallback != null) upCallback();
    }
    if (rb.position.y <= -mapBoundaries.y + ballRadius && rb.velocity.y < 0)
    {
      genericCollisionResolver(rb, Vector2.down, rb.velocity);

      if (downCallback != null) downCallback();
    }
  }

  public static bool isOutOfBounds(GameObject go) {
    float ballRadius = go.transform.lossyScale.y / 2f;
    Vector3 mapBoundaries = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

    if (go.transform.position.x >= mapBoundaries.x + ballRadius) {
      return true;
    }
    if (go.transform.position.x <= -mapBoundaries.x - ballRadius) {
      return true;
    }
    if (go.transform.position.y >= mapBoundaries.y + ballRadius) {
      return true;
    }
    if (go.transform.position.y <= -mapBoundaries.y - ballRadius) {
      return true;
    }

    return false;
  }

  static void genericCollisionResolver(Rigidbody2D rb, Vector2 normal, Vector2 lastVelocity)
  {
    rb.velocity = Vector2.Reflect(lastVelocity, normal);
  }

  public static void genericCollisionHandler(GameObject go, Collision2D collision, Vector2 lastVelocity)
  {
    Rigidbody2D rb = go.GetComponent<Rigidbody2D>();
    Vector2 lastNormal = Vector2.zero;

    for (int i = 0; i < collision.contactCount; i++)
    {
      ContactPoint2D contact = collision.GetContact(i);
      Debug.DrawLine(go.transform.position, rb.position + contact.normal, Color.yellow, 3f);
      Debug.DrawLine(go.transform.position, rb.position + rb.velocity.normalized, Color.red, 3f);
      Debug.DrawLine(go.transform.position, rb.position + lastVelocity.normalized, Color.blue, 3f);

      lastNormal = contact.normal;
    }

    // rb.velocity = Vector2.Reflect(lastVelocity, lastNormal);
    genericCollisionResolver(rb, lastNormal, lastVelocity);
  }

}

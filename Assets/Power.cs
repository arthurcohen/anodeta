using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Power : MonoBehaviour {
    public string functionName;
    public int weight = 1;
    private float sidewaysMove = 0f;

    void Start() {
        Destroy(gameObject, 15);
    }

    void Update()
    {
        move();
    }

    private void move() {
        sidewaysMove += (UnityEngine.Random.Range(-1f, 1f) - transform.position.x / 3f) / 10f;
        transform.Translate(new Vector2(Mathf.Clamp(sidewaysMove, -0.5f, 0.5f), -1) * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag != "Player") return;
        Debug.Log(functionName);
        Destroy(gameObject);
        GameObject.FindObjectOfType<GenericGameController>().SendMessage(functionName);
    }
}

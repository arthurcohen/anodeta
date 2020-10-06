using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GenericGameController : MonoBehaviour {
    // Start is called before the first frame update
    public float paddleControllerHeight = -2.7f;
    public GameObject ballPrefab;
    void Start()
    {
        createNewBall();
        Debug.DrawLine(new Vector2(-2.5f, paddleControllerHeight), new Vector2(2.5f, paddleControllerHeight), Color.red, 1000);
    }

    // Update is called once per frame
    void Update()
    {
        touchControllerRoutine();
        if (Input.GetMouseButtonDown(1)) {
            Debug.Log("temp speed");
            setPaddleTemporaryMaxSpeed(15f, 3f);
        }
        checkDeath();
        checkEndGame();
    }


    private void checkEndGame() {
        bool shouldEnd = true;

        foreach(GenericBrick brick in GameObject.FindObjectsOfType<GenericBrick>()) {
            if (brick.healthPoints > 0) shouldEnd = false;
        }
        
        if (!shouldEnd) return;

        Debug.Log("All bricks died");
        resetGame();
    }

    private void touchControllerRoutine() {
        Vector2 touchPosition = Vector2.zero;
        bool touched = false;

        if (Input.touchCount > 0) {
            foreach(Touch touch in Input.touches) {
                touchPosition += (Vector2) Camera.main.ScreenToWorldPoint(touch.position); 
                touched = true;
            }

            touchPosition = touchPosition / Input.touchCount;
        } else if (Input.GetMouseButton(0)) {
            touchPosition= Camera.main.ScreenToWorldPoint(Input.mousePosition); 
            touched = true;
        }

        if (!touched) return;

        Debug.DrawLine(Vector2.zero, touchPosition, Color.cyan);

        if (touchPosition.y <= paddleControllerHeight) {
            movePaddle(touchPosition);
        } else {
            launchAllBalls();
            createGravity(touchPosition);
        }

    }

    private void movePaddle(Vector2 controllerPosition) {
        GameObject.FindObjectOfType<Paddle>().move(controllerPosition);
    }

    public void createGravity(Vector2 position) {
        foreach(Ball ball in GameObject.FindObjectsOfType<Ball>()) {
            ball.gravityPosition = position;
            // Debug.Log(position);
        }
    }

    private void launchAllBalls() {
        Ball[] balls = GameObject.FindObjectsOfType<Ball>();

        foreach(Ball ball in balls) {
            ball.launch();
        }
    }

    public void setPaddleTemporaryMaxSpeed(float newMaxSpeed, float howLong) {
        StartCoroutine(corroutinePaddleTemporaryMaxSpeed(newMaxSpeed, howLong));
    }

    IEnumerator corroutinePaddleTemporaryMaxSpeed(float newMaxSpeed, float howLong) {
        Paddle paddle = GameObject.FindObjectOfType<Paddle>();
        paddle.setSpeed(newMaxSpeed);
        yield return new WaitForSeconds(howLong);
        paddle.resetSpeed();
    }

    public void resetGame() {
        foreach(Ball ball in GameObject.FindObjectsOfType<Ball>()) {
            ball.resetBall(false);
        }

        foreach(GenericBrick brick in GameObject.FindObjectsOfType<GenericBrick>()) {
            brick.resetBrick();
        }

        foreach(Power power in GameObject.FindObjectsOfType<Power>()) {
            Destroy(power.gameObject);
        }
    }

    public void increaseBallSpeed() {
        foreach(Ball ball in GameObject.FindObjectsOfType<Ball>()) {
            ball.speedModifier *= 1.3f;
        }
    }

    public void decreaseBallSpeed() {
        foreach(Ball ball in GameObject.FindObjectsOfType<Ball>()) {
            ball.speedModifier *= 1f/1.3f;
        }
    }

    public void loseHealth() {
        Debug.Log("healts lost");
    }

    private void createNewBall() {
        GameObject ball = Instantiate(ballPrefab, Vector2.zero, Quaternion.identity);
    }

    private void checkDeath() {
        if (GameObject.FindObjectsOfType<Ball>().Length != 0) return;

        loseHealth();
        createNewBall();
    }

    public Vector2 detectMapBoundaries() {
        return Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
    }
}

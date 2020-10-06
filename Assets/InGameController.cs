using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameController : MonoBehaviour
{
    private Boolean isPaused = false;

    // Update is called once per frame
    void Update()
    {
        updateGameSpeed();
    }

    
    private void updateGameSpeed() {
        Time.timeScale = UnityEngine.Mathf.Lerp(Time.timeScale, isPaused ? 0f : 1f, 0.05f);
        
        if (isPaused && Time.timeScale < 0.05f) Time.timeScale = 0;
        if (!isPaused && Time.timeScale > 0.95f) Time.timeScale = 1;
    }

    
    public void play() {
        print("play");
        isPaused = false;
    }
    
    public void pause() {
        print("pause");
        isPaused = true;
    }

    public void loadScene(int index) {
        SceneManager.LoadScene(index, LoadSceneMode.Single);
    }
    
    public void loadMainMenu() {
        loadScene(0);
    }

    public void restartGame() {
        GameObject.FindObjectOfType<GenericGameController>().resetGame();
    }
}

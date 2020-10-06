using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    public void playGame() {
        int sceneToPlay = UnityEngine.Random.Range(0, SceneManager.sceneCountInBuildSettings - 1);
        Debug.Log(sceneToPlay);
        SceneManager.LoadScene(sceneToPlay + 1, LoadSceneMode.Single);
    }

    public void quitGame() {
        Debug.Log("quit");
        Application.Quit();
    }
}

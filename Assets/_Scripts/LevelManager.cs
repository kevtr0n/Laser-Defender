using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelManager : MonoBehaviour {
    // Loads a specified level.
    public void LoadLevel(string name)
    {
        SceneManager.LoadScene(name);
        ScoreKeeper.Reset();
    }
    // Calls the next level in the build.
    public void LoadNextLevel()
    {
        Scene activeScene = SceneManager.GetActiveScene();
        int activeSceneBuildIndex = activeScene.buildIndex;
        SceneManager.LoadScene(++activeSceneBuildIndex);
    }
    // Requests to quit.
    public void QuitRequest()
    {
        Debug.Log("I want to quit!");
        Application.Quit();
    }
}

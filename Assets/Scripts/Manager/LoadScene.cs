using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public static LoadScene instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // used to get to the main menu from any scene 
    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    // loads the next level if it detects one in the next scene index position
    public void LoadNextLevel()
    {
        int nextBuildIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextBuildIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextBuildIndex);
        }
    }

    // used to get the current scene index 
    public int GetLevelIndex()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(GetLevelIndex());
    }

    public void LoadAnyScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}

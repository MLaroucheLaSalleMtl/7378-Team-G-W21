using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Hayedeh Worked on this Script

public class MainMenuControl : MonoBehaviour
{

    public void StartGame()
    {
        LoadScene.instance.LoadNextLevel();
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
    }
}
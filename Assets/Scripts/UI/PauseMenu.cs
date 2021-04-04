using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    GameManager manager;
    public static PauseMenu instance = null;
    public GameObject pauseMenuUI;

    private void Awake()
    {
        manager = GameManager.instance;
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Pause(bool isPaused)
    {
        if (isPaused)
        {
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void OnResume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
    }

    public void OnMainMenu()
    {
        Time.timeScale = 1f;
        manager.RoundUnsubs();
        manager.MatchEnded();
    }

    public void OnQuit()
    {
        manager.ExitGame();
    }
}

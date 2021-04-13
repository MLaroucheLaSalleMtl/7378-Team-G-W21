using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    GameManager manager;
    TrainingManager trainingManager;
    TutorialManager tutorialManager;
    CharacterSelection characterSelection;
    StageSelection stageSelection;

    public static PauseMenu instance = null;
    public GameObject pauseMenuUI;

    private void Awake()
    {
        manager = GameManager.instance;
        trainingManager = TrainingManager.instance;
        tutorialManager = TutorialManager.instance;
        characterSelection = CharacterSelection.instance;
        stageSelection = StageSelection.instance;

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

        if (GameObject.Find("GameManager") != null)
        {
            manager.RoundUnsubs();
            manager.MatchEnded();
            LoadScene.instance.LoadMainMenu(); 
        }
        else
        {
            characterSelection.SelfDestruction();
            stageSelection.SelfDestruction();
            LoadScene.instance.LoadMainMenu();
        }
    }

    public void OnQuit()
    {
        if(GameObject.Find("GameManager") != null)
        {
            manager.ExitGame();
        }
        else if(GameObject.Find("TrainingManager") != null)
        {
            trainingManager.ExitGame();
        }
        else if (GameObject.Find("TutorialManager") != null)
        {
            tutorialManager.ExitGame();
        }
    }
}

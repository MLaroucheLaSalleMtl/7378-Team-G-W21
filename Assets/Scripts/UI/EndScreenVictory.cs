using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScreenVictory : MonoBehaviour
{
    [Header("Instances")]
    [SerializeField] private GameManager manager;

    [Header("End Screen Manager")]
    [SerializeField] private GameObject endScreenPanel;
    [SerializeField] private GameObject[] winnerImages;
    [SerializeField] private GameObject[] loserImages;
    [SerializeField] private bool player1Winner;
    [SerializeField] private bool panelIsOn;

    void Start()
    {
        manager = GameManager.instance;
        ClearImagesDisplay();
    }

    void Update()
    {
        PanelDisplay();
    }

    public void ClearImagesDisplay()
    {
        for(int i = 0; i < winnerImages.Length; i++)
        {
            winnerImages[i].SetActive(false);
        }
        for (int i = 0; i < loserImages.Length; i++)
        {
            loserImages[i].SetActive(false);
        }
    }

    public bool CheckIfPlayer1Won()
    {
        player1Winner = manager.roundCounterP1 > manager.roundCounterP2 ? true : false;
        return player1Winner;
    }

    public void PanelDisplay()
    {
        if (manager.isMatchEnded)
        {
            endScreenPanel.SetActive(true);
            ImagesDisplay();
            Time.timeScale = 0f;
        }
        else
        {
            endScreenPanel.SetActive(false);
        }
    }

    public void ImagesDisplay()
    {
        if (CheckIfPlayer1Won())
        {
            winnerImages[manager.player1.GetComponent<FighterStatus>().playerID].SetActive(true);
            loserImages[manager.player2.GetComponent<FighterStatus>().playerID].SetActive(true);
        }
        else
        {
            winnerImages[manager.player2.GetComponent<FighterStatus>().playerID].SetActive(true);
            loserImages[manager.player1.GetComponent<FighterStatus>().playerID].SetActive(true);
        }
    }

    public void OnRematch()
    {
        Time.timeScale = 1f;
        manager.Rematch();
        manager.isMatchEnded = false;
        manager.isRoundEnded = false;
    }

    public void OnMainMenu()
    {
        manager.MatchEnded();
        LoadScene.instance.LoadMainMenu();
    }

    public void OnQuit()
    {
        manager.ExitGame();
    }
}

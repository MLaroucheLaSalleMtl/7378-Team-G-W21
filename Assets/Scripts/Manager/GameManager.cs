using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    private List<FighterStatus> fighterStatus = new List<FighterStatus>();

    //private CharacterSelection characterSelection;

    //Properties
    public float timeCounter = 0f;
    private bool roundIsEnded = false;

    public int roundCounterP1Int { get; set; }
    public int roundCounterP2Int { get; set; }

    public bool matchFullyEnded { get; set; } = false;

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

    void Start()
    {
        SetFighterStatus();
        //SetFighterStatusTest(characterSelection.playerTest); ;
    }
    
    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SetFighterStatus();
        //SetFighterStatusTest(characterSelection.playerTest);
    }

    /*
    public void SetFighterStatusTest(GameObject playerSelected)
    {
        GameObject player1 = playerSelected;
        FighterStatus currentP1FighterStatus = player1.GetComponent<FighterStatus>();
        fighterStatus.Add(currentP1FighterStatus);
        currentP1FighterStatus.WhatPlayer(1);

        GameObject player2 = GameObject.FindGameObjectWithTag("Player2");
        FighterStatus currentP2FighterStatus = player2.GetComponent<FighterStatus>();
        fighterStatus.Add(currentP2FighterStatus);
    }
    */

    public void SetFighterStatus()
    {
        GameObject player1 = GameObject.FindGameObjectWithTag("Player1");
        FighterStatus currentP1FighterStatus = player1.GetComponent<FighterStatus>();
        fighterStatus.Add(currentP1FighterStatus);

        GameObject player2 = GameObject.FindGameObjectWithTag("Player2");
        FighterStatus currentP2FighterStatus = player2.GetComponent<FighterStatus>();
        fighterStatus.Add(currentP2FighterStatus);
    }

    void Update()
    {
        RoundEnded();
    }

    public void RoundEnded()
    {
        if(roundCounterP1Int == 2 || roundCounterP2Int == 2)
        {
            matchFullyEnded = true;
            StartCoroutine(waitForEnd());
        }

        if (fighterStatus[0].health <= 0)
        {
            roundIsEnded = true;    
            roundCounterP2Int++;
            LoadNewRound();

        }
        if (fighterStatus[1].health <= 0)
        {
            roundIsEnded = true;
            roundCounterP1Int++;
            LoadNewRound();
        }
    }

    public void LoadNewRound()
    {
        if (roundIsEnded)   
        {
            roundIsEnded = false;
        }
        fighterStatus.Clear();
        LoadScene.instance.ReloadScene();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void MatchEnded()
    { 
        SceneManager.sceneLoaded -= OnSceneLoaded;
        LoadScene.instance.LoadMainMenu();
        Destroy(gameObject);
    }

    IEnumerator waitForEnd()
    {
        yield return new WaitForSecondsRealtime(5);
        MatchEnded();
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

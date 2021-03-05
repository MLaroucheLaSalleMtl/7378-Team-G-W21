using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    private List<FighterStatus> fighterStatus = new List<FighterStatus>();

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
    }
    
    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SetFighterStatus();
    }
    
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
        if (Input.GetKeyDown(KeyCode.Escape))   
        {
            ExitGame();
        }

        RoundEnded();
    }

    public void RoundEnded()
    {
        if(roundCounterP2Int == 2)
        {
            matchFullyEnded = true;
            StartCoroutine(waitForEnd());
        }

        if (fighterStatus[1].health <= 0)
        {
            roundIsEnded = true;
            roundCounterP2Int++;
            LoadNewRound();
        }

        //checks which player won the round, then increment a counter by 1
        //update UI
        //resets the scene
        //check if the match is ended
    }

    public void LoadNewRound()
    {
        if (roundIsEnded)   
        {
            roundIsEnded = false;
        }
        fighterStatus[1].health = 100f;
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

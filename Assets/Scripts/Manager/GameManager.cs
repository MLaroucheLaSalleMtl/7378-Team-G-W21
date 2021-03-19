using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    [Header("Input Manager")]
    [SerializeField] private MyCharacterController[] playerList;
    private int playerCount = 0;

    [Header("User Interface")]
    public float timeCounter = 0f;
    public Image[] healthBar;
    public Text[] ratioText;

    [Header("Player Manager")]
    [SerializeField] private GameObject[] charactersArray;
    public GameObject[] charactersSelected;
    private List<FighterStatus> fighterStatus = new List<FighterStatus>();
    private FighterStatus[] playerFighterStatus;
    private CharacterSelection characterSelection;
    private Vector3 spawnPositionP1 = new Vector3(2.56f, 0.07f, -5.76f);
    private Vector3 spawnPositionP2 = new Vector3(-2.56f, 0.07f, -5.76f);
    private Vector3 spawnRotationP1 = new Vector3(0, 0, 0);
    private Vector3 spawnRotationP2 = new Vector3(0, 0, 0);

    //Stage Manager

    //Match Manager
    private string playModeSelected;
    public int roundCounterP1Int { get; set; }
    public int roundCounterP2Int { get; set; }
    public bool roundIsEnded = false;
    public bool matchFullyEnded { get; set; } = false;


    public MyCharacterController GetPlayer()
    {
        if(playerList.Length > playerCount)
        {
            MyCharacterController pl = playerList[playerCount];
            playerCount++;
            return pl;
        }
        else
        {
            return null;
        }
    }

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
        for (int i = 0; i < charactersSelected.Length; i++)
        {
            playerFighterStatus[i] = charactersSelected[i].GetComponent<FighterStatus>();
            fighterStatus.Add(playerFighterStatus[i]);
        }

        /*
        GameObject player1 = GameObject.FindGameObjectWithTag("Player1");
        FighterStatus currentP1FighterStatus = player1.GetComponent<FighterStatus>();
        fighterStatus.Add(currentP1FighterStatus);

        GameObject player2 = GameObject.FindGameObjectWithTag("Player2");
        FighterStatus currentP2FighterStatus = player2.GetComponent<FighterStatus>();
        fighterStatus.Add(currentP2FighterStatus);
        */
    }

    public void InstantiatePlayers()
    {
        switch (characterSelection.GetPlayMode())
        {

        }
    }

    public void SetUI(GameObject[] charactersSelected)
    {

    }

    public void UIUpdate()
    {
        for(int i = 0; i < charactersSelected.Length; i++)
        {
            healthBar[i].fillAmount = charactersSelected[i].GetComponent<FighterStatus>().health / 100;
            ratioText[i].text = (charactersSelected[i].GetComponent<FighterStatus>().health).ToString("0");
        }
    }

    void FixedUpdate()
    {
        UIUpdate();
        RoundEnded();
    }

    public void RoundEnded()
    {
        if(roundCounterP1Int == 2 || roundCounterP2Int == 2)
        {
            matchFullyEnded = true;
            StartCoroutine(WaitForEnd());
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

    IEnumerator WaitForEnd()
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    [Header("Input Manager")]
    [SerializeField] private int playerCount = 0;

    [Header("User Interface")]
    [SerializeField] private Image healthBarP1 = null;
    [SerializeField] private Image healthBarP2 = null;
    [SerializeField] private Text ratioTextP1 = null;
    [SerializeField] private Text ratioTextP2 = null;
    [SerializeField] private Text counterText;
    public float timeCounter = 99f;
    [SerializeField] private Image specialBarP1 = null;
    [SerializeField] private Image specialBarP2 = null;
    [SerializeField] private Text specialRatioTextP1 = null;
    [SerializeField] private Text specialRatioTextP2 = null;

    [Header("Player Manager")]
    [SerializeField] private CharacterSelection characterSelection;
    [SerializeField] private List<GameObject> charactersSelected = new List<GameObject>();
    [SerializeField] private List<FighterStatus> fighterStatus = new List<FighterStatus>();
    [SerializeField] private Vector3 spawnPositionP1 = new Vector3(2.56f, 0.07f, -5.76f);
    [SerializeField] private Vector3 spawnPositionP2 = new Vector3(-2.56f, 0.07f, -5.76f);
    [SerializeField] private GameObject player1 = null;
    [SerializeField] private GameObject player2 = null;

    [Header("Stage Manager")]
    [SerializeField] private StageSelection stageSelection;
    [SerializeField] private GameObject stageSelected = null;
    [SerializeField] private Vector3 spawnPositionStage = new Vector3(0, 0, 0);

    //Match Manager
    [SerializeField] private string playModeSelected;
    public int roundCounterP1Int { get; set; }
    public int roundCounterP2Int { get; set; }
    public bool roundIsEnded = false;
    public bool matchFullyEnded { get; set; } = false;

    public MyCharacterController GetPlayer()
    {
        if (charactersSelected.Count > playerCount)
        {
            if (playerCount == 0)
            {
                MyCharacterController pl = player1.GetComponent<MyCharacterController>();
                playerCount++;
                return pl;
            }
            else if (playerCount == 1)
            {
                MyCharacterController pl = player2.GetComponent<MyCharacterController>();
                playerCount++;
                return pl;
            }
            else
            {
                return null;
            }
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
        characterSelection = CharacterSelection.instance;
        stageSelection = StageSelection.instance;

        GetRoundReady();
    }
    
    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GetRoundReady();
    }

    public void GetRoundReady()
    {
        CounterClear();
        ClearTimer();
        SetPlayers();
        SetPlayMode();
        InstantiatePlayers();
        SetFighterStatus();
        SetUI();
    }

    public void RoundSubs()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void RoundUnsubs()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void SetPlayers()
    {
        charactersSelected = characterSelection.GetCharacters().GetClone();
    }

    public void SetStage()
    {
        stageSelected = stageSelection.GetStage();
    }

    public void SetPlayMode()
    {
        playModeSelected = characterSelection.GetPlayMode();
    }

    public void InstantiatePlayers()
    {
        switch (playModeSelected)
        {
            case "2P":
                Instantiate(charactersSelected[0], spawnPositionP1, Quaternion.identity);
                Instantiate(charactersSelected[1], spawnPositionP2, Quaternion.identity);
                player1 = GameObject.FindGameObjectWithTag("Player1");
                player2 = GameObject.FindGameObjectWithTag("Player2");
                break;
            default:
                break;
        }
    }

    public void InstantiateStage()
    {
        Instantiate(stageSelected, spawnPositionStage, Quaternion.identity);
    }

    public void SetFighterStatus()
    {
        switch (playModeSelected)
        {
            case "2P":
                fighterStatus.Add(player1.GetComponent<FighterStatus>());
                fighterStatus.Add(player2.GetComponent<FighterStatus>());
                break;
            default:
                break;
        }
    }

    public void SetUI()
    {
        healthBarP1 = GameObject.FindGameObjectWithTag("HealthBarP1").GetComponent<Image>();
        healthBarP2 = GameObject.FindGameObjectWithTag("HealthBarP2").GetComponent<Image>();

        ratioTextP1 = GameObject.FindGameObjectWithTag("RatioTextP1").GetComponent<Text>();
        ratioTextP2 = GameObject.FindGameObjectWithTag("RatioTextP2").GetComponent<Text>();

        specialBarP1 = GameObject.FindGameObjectWithTag("SpecialBarP1").GetComponent<Image>();
        specialBarP2 = GameObject.FindGameObjectWithTag("SpecialBarP2").GetComponent<Image>();

        specialRatioTextP1 = GameObject.FindGameObjectWithTag("SpecialRatioTextP1").GetComponent<Text>();
        specialRatioTextP2 = GameObject.FindGameObjectWithTag("SpecialRatioTextP2").GetComponent<Text>();

        counterText = GameObject.FindGameObjectWithTag("TimerTxt").GetComponent<Text>();
    }

    public void UIUpdate()
    {
        HealthBarUpdate();
        SpecialBarUpdate();
    }

    public void HealthBarUpdate()
    {
        healthBarP1.fillAmount = fighterStatus[0].health / 100;
        ratioTextP1.text = fighterStatus[0].health.ToString("0");

        healthBarP2.fillAmount = fighterStatus[1].health / 100;
        ratioTextP2.text = fighterStatus[1].health.ToString("0");
    }

    public void SpecialBarUpdate()
    {
        specialBarP1.fillAmount = fighterStatus[0].specialPoints / 100;
        specialRatioTextP1.text = fighterStatus[0].specialPoints.ToString("0");

        specialBarP2.fillAmount = fighterStatus[1].specialPoints / 100;
        specialRatioTextP2.text = fighterStatus[1].specialPoints.ToString("0");
    }

    void FixedUpdate()
    {
        Timer();
        UIUpdate();
        RoundEnded();
    }

    public void RoundEnded()
    {
        if(roundCounterP1Int == 2 || roundCounterP2Int == 2)
        {
            matchFullyEnded = true;
            RoundUnsubs();
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
        RoundSubs();
    }

    public void MatchEnded()
    {
        LoadScene.instance.LoadMainMenu();
        characterSelection.SelfDestruction();
        Destroy(gameObject);
    }

    IEnumerator WaitForEnd()
    {
        yield return new WaitForSecondsRealtime(5);
        MatchEnded();
    }

    public void Timer()
    {
        timeCounter -= Time.deltaTime;
        TimerDisplay();
    }

    public void TimerDisplay()
    {
        counterText.text = timeCounter.ToString("F1");
    }

    public void ClearTimer()
    {
        timeCounter = 99f;
    }

    public void CounterClear()
    {
        playerCount = 0;
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

public static class Extentions
{
   public static List<T> GetClone<T>(this List<T> source)
    {
        return source.GetRange(0, source.Count);
    }
}

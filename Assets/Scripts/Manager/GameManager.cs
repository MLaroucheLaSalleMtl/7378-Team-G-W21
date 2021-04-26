using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

//Nicholaos And Eduardo Worked on this Script 

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    [Header("Instances")]
    [SerializeField] private CharacterSelection characterSelection;
    [SerializeField] private StageSelection stageSelection;
    [SerializeField] private SettingsSelection settingsSelection;

    [Header("Input Manager")]
    [SerializeField] private int playerCount = 0;

    [Header("User Interface")]
    [SerializeField] private Image healthBarP1 = null;
    [SerializeField] private Image healthBarP2 = null;
    [SerializeField] private Text ratioTextP1 = null;
    [SerializeField] private Text ratioTextP2 = null;
    [SerializeField] private Text counterText;
    [SerializeField] private Image specialBarP1 = null;
    [SerializeField] private Image specialBarP2 = null;
    [SerializeField] private Text specialRatioTextP1 = null;
    [SerializeField] private Text specialRatioTextP2 = null;

    [Header("Player Manager")]
    [SerializeField] private List<GameObject> charactersSelected = new List<GameObject>();
    [SerializeField] private List<FighterStatus> fighterStatus = new List<FighterStatus>();
    [SerializeField] private Vector3 spawnPositionP1 = new Vector3(2.56f, 0.07f, -5.76f);
    [SerializeField] private Vector3 spawnPositionP2 = new Vector3(-2.56f, 0.07f, -5.76f);
    public GameObject player1 = null;
    public GameObject player2 = null;

    [Header("Stage Manager")]
    [SerializeField] private GameObject stageSelected = null;
    [SerializeField] private Vector3 spawnPositionStage = new Vector3(0, 0, 0);

    [Header("Match Manager")]
    public float timeCounter = 99f;
    [SerializeField] private float timeToVictoryScreen = 3.5f;
    [SerializeField] private string playModeSelected;
    public int roundCounterP1;
    public int roundCounterP2;
    public bool isMatchEnded = false;
    private bool isInVictoryPanel = false;

    public System.Action onVictoryDanceFinish;

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
        GetInstancesReady();
        GetRoundReady();
    }

    void FixedUpdate()
    {
        Timer();
        UIUpdate();
        RoundEnded();
    }

    public MyCharacterController GetPlayer()
    {
        var keyboard = Keyboard.current;
        var gamepad = Gamepad.current;

        if (charactersSelected.Count > playerCount)
        {
            if (playerCount == 0)
            {
                if(keyboard != null || gamepad != null)
                {
                    MyCharacterController pl = player1.GetComponent<MyCharacterController>();
                    playerCount++;
                    return pl;
                }
                else
                {
                    return null;
                }
            }
            else if (playerCount == 1)
            {
                if(keyboard != null || gamepad != null)
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

        else
        {
            return null;
        }
        
    }

    public void GetInstancesReady()
    {
        characterSelection = CharacterSelection.instance;
        stageSelection = StageSelection.instance;
        settingsSelection = SettingsSelection.instance;
    }
    
    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GetRoundReady();
    }

    public void GetRoundReady()
    {
        isMatchEnded = false;
        CounterClear();
        SetTimer();
        SetStage();
        InstantiateStage();
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
        healthBarP1.fillAmount = fighterStatus[0].health / settingsSelection.health;
        ratioTextP1.text = fighterStatus[0].health.ToString("0");

        healthBarP2.fillAmount = fighterStatus[1].health / settingsSelection.health;
        ratioTextP2.text = fighterStatus[1].health.ToString("0");
    }

    public void SpecialBarUpdate()
    {
        specialBarP1.fillAmount = fighterStatus[0].specialPoints / settingsSelection.specialPoints;
        specialRatioTextP1.text = fighterStatus[0].specialPoints.ToString("0");

        specialBarP2.fillAmount = fighterStatus[1].specialPoints / settingsSelection.specialPoints;
        specialRatioTextP2.text = fighterStatus[1].specialPoints.ToString("0");
    }

    public void RoundEnded()
    {
        if (fighterStatus[0].health <= 0)
        {
            roundCounterP2++;
            if (roundCounterP2 >= 2)
            {
                if (!isInVictoryPanel)
                {
                    fighterStatus[1].VictoryDance();
                    Invoke("OnVictoryDanceFinish", 4f);
                    RoundUnsubs();
                    isInVictoryPanel = true;
                }
            }
            else
            {
                LoadNewRound();
            }
        }

        else if (fighterStatus[1].health <= 0)
        {
            roundCounterP1++;
            if (roundCounterP1 >= 2)
            {
                if (!isInVictoryPanel)
                {
                    fighterStatus[0].VictoryDance();
                    Invoke("OnVictoryDanceFinish", 4f);
                    RoundUnsubs();
                    isInVictoryPanel = true;
                }
            }
            else
            {
                LoadNewRound();
            }
        }
    }

    private void OnVictoryDanceFinish()
    {
        onVictoryDanceFinish?.Invoke();
    }

    public void LoadNewRound()
    {
        if (roundCounterP1 != 0 || roundCounterP2 != 0)
        {
            RoundUnsubs();
        }

        fighterStatus.Clear();
        LoadScene.instance.ReloadScene();
        RoundSubs();
    }

    public void SetMatchEndedTrue()
    {
        isMatchEnded = true;
    }


    public void MatchEnded()
    {
        characterSelection.SelfDestruction();
        stageSelection.SelfDestruction();
        Destroy(gameObject);
    }

    public void SetTimer()
    {
        timeCounter = settingsSelection.timer;
    }

    public void Timer()
    {
        timeCounter -= Time.deltaTime;
        TimesUp();
        TimerDisplay();
    }

    public void TimesUp()
    {
        if(timeCounter <= 0)
        {
            if(fighterStatus[0].health > fighterStatus[1].health)
            {
                roundCounterP1++;
                if(roundCounterP1 < 2)
                {
                    LoadNewRound();
                }
            }
            else
            {
                roundCounterP2++;
                if (roundCounterP2 < 2)
                {
                    LoadNewRound();
                }
            }
        }
    }

    public void TimerDisplay()
    {
        counterText.text = timeCounter.ToString("F1");
    }

    public void CounterClear()
    {
        playerCount = 0;
    }

    public void Rematch()
    {
        isMatchEnded = false;
        roundCounterP1 = 0;
        roundCounterP2 = 0;
        RoundUnsubs();
        LoadNewRound();
        isInVictoryPanel = false;
        Time.timeScale = 1;
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

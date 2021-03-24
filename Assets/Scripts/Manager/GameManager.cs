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
    [SerializeField] private int playerCount = 0;

    [Header("User Interface")]
    public float timeCounter = 0f;
    public Image[] healthBar;
    public Text[] ratioText;

    [Header("Player Manager")]
    [SerializeField] private CharacterSelection characterSelection;
    [SerializeField] private List<GameObject> charactersSelected = new List<GameObject>();
    [SerializeField] private List<FighterStatus> fighterStatus = new List<FighterStatus>();
    [SerializeField] private Vector3 spawnPositionP1 = new Vector3(2.56f, 0.07f, -5.76f);
    [SerializeField] private Vector3 spawnPositionP2 = new Vector3(-2.56f, 0.07f, -5.76f);

    //Stage Manager

    //Match Manager
    [SerializeField] private string playModeSelected;
    public int roundCounterP1Int { get; set; }
    public int roundCounterP2Int { get; set; }
    public bool roundIsEnded = false;
    public bool matchFullyEnded { get; set; } = false;

    public MyCharacterController GetPlayer()
    {
        /*
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
        */

        if (playerList.Length > playerCount)
        {
            if(playerCount == 0)
            {
                if(charactersSelected[0].GetComponent<FighterStatus>().playerID == 0)
                {
                    MyCharacterController pl = playerList[0];
                    Debug.Log("BP1");
                    playerCount++;
                    return pl;
                }
                if (charactersSelected[0].GetComponent<FighterStatus>().playerID == 1)
                {
                    MyCharacterController pl = playerList[1];
                    Debug.Log("RP1");
                    playerCount++;
                    return pl;
                }
                if (charactersSelected[0].GetComponent<FighterStatus>().playerID == 2)
                {
                    MyCharacterController pl = playerList[2];
                    Debug.Log("FP1");
                    playerCount++;
                    return pl;
                }
                else
                {
                    return null;
                }
            }
            else if(playerCount == 1)
            {
                if (charactersSelected[1].GetComponent<FighterStatus>().playerID == 0)
                {
                    MyCharacterController pl = playerList[3];
                    Debug.Log("BP2");
                    playerCount++;
                    return pl;
                }
                if (charactersSelected[1].GetComponent<FighterStatus>().playerID == 1)
                {
                    MyCharacterController pl = playerList[4];
                    Debug.Log("RP2");
                    playerCount++;
                    return pl;
                }
                if (charactersSelected[1].GetComponent<FighterStatus>().playerID == 2)
                {
                    MyCharacterController pl = playerList[5];
                    Debug.Log("FP2");
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

        characterSelection = CharacterSelection.instance;

        SetPlayers();
    }

    void Start()
    {
        /*
        characterSelection = CharacterSelection.instance;

        SetPlayers();
        */
        SetPlayMode();
        InstantiatePlayers();
        SetFighterStatus();
    }
    
    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SetPlayers();
        SetPlayMode();
        InstantiatePlayers();
        SetFighterStatus();
    }

    public void SetPlayers()
    {
        charactersSelected = characterSelection.GetCharacters().GetClone();
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
                break;
            case "1P":
                Instantiate(charactersSelected[0], spawnPositionP1, Quaternion.identity);
                break;
            default:
                break;
        }
    }

    public void SetFighterStatus()
    {
        switch (playModeSelected)
        {
            case "2P":
                fighterStatus.Add(charactersSelected[0].GetComponent<FighterStatus>());
                fighterStatus.Add(charactersSelected[1].GetComponent<FighterStatus>());
                break;
            case "1P":
                fighterStatus.Add(charactersSelected[0].GetComponent<FighterStatus>());
                break;
            default:
                break;
        }
    }

    public void UIUpdate()
    {
        for(int i = 0; i < charactersSelected.Count; i++)
        {
            healthBar[i].fillAmount = charactersSelected[i].GetComponent<FighterStatus>().health / 100;
            ratioText[i].text = charactersSelected[i].GetComponent<FighterStatus>().health.ToString("0");
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

public static class Extentions
{
   public static List<T> GetClone<T>(this List<T> source)
    {
        return source.GetRange(0, source.Count);
    }
}

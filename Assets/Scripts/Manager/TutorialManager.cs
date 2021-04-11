using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class TutorialManager : MonoBehaviour
{
    public static TutorialManager instance = null;

    [Header("User Interface")]
    [SerializeField] private GameObject[] popUps;
    private int popUpIndex;
    [SerializeField] private float timeToMenu = 4f;

    [Header("Player Manager")] 
    [SerializeField] private CharacterSelection characterSelection;
    [SerializeField] private List<GameObject> charactersSelected = new List<GameObject>();
    [SerializeField] private Vector3 spawnPositionP1 = new Vector3(2.56f, 0.07f, -5.76f);
    [SerializeField] private Vector3 spawnPositionP2 = new Vector3(-2.56f, 0.07f, -5.76f);
    [SerializeField] private GameObject player1 = null;
    [SerializeField] private GameObject player2 = null;

    [Header("Stage Manager")]
    [SerializeField] private StageSelection stageSelection;
    [SerializeField] private GameObject stageSelected = null;
    [SerializeField] private Vector3 spawnPositionStage = new Vector3(0, 0, 0);

    public MyCharacterController GetPlayer()
    {
        MyCharacterController pl = player1.GetComponent<MyCharacterController>();
        return pl;
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
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

        popUpIndex = 0;

        GetRoundReady();
    }

    void Update()
    {
        for(int i = 0; i < popUps.Length; i++)
        {
            if(i == popUpIndex)
            {
                popUps[i].SetActive(true);
            }
            else
            {
                popUps[i].SetActive(false);
            }
        }

        if (popUpIndex == 0)
        {
            if (player1.GetComponent<MyCharacterController>().inputHor != 0)
            {
                popUpIndex++;
            }
        }
        else if (popUpIndex == 1)
        {
            if (player1.GetComponent<MyCharacterController>().isBlocking)
            {
                popUpIndex++;
            }
        }
        else if (popUpIndex == 2)
        {
            if (player1.GetComponent<MyCharacterController>().inputPunch)
            {
                popUpIndex++;
            }
        }
        else if (popUpIndex == 3)
        {
            if (player1.GetComponent<MyCharacterController>().inputKick)
            {
                popUpIndex++;
            }
        }
        else if (popUpIndex == 4)
        {
            SpecialPump();

            if (player1.GetComponent<MyCharacterController>().inputSpecialAttack)
            {
                popUpIndex++;
            }
        }
        else if (popUpIndex == 5)
        {
            StartCoroutine(WaitForEnd());
        }
    }

    public void GetRoundReady()
    {
        SetStage();
        InstantiateStage();
        SetPlayers();
        InstantiatePlayers();
    }

    public void SetStage()
    {
        stageSelected = stageSelection.GetStage();
    }

    public void SetPlayers()
    {
        charactersSelected = characterSelection.GetCharacters().GetClone();
    }

    public void InstantiateStage()
    {
        Instantiate(stageSelected, spawnPositionStage, Quaternion.identity);
    }

    public void InstantiatePlayers()
    {
        Instantiate(charactersSelected[0], spawnPositionP1, Quaternion.identity);
        Instantiate(charactersSelected[1], spawnPositionP2, Quaternion.identity);
        player1 = GameObject.FindGameObjectWithTag("Player1");
        player2 = GameObject.FindGameObjectWithTag("Player2");
    }

    public void LoadMainMenu()
    {
        LoadScene.instance.LoadMainMenu();
    }

    public void SpecialPump()
    {
        player1.GetComponent<FighterStatus>().specialPoints = 100;
    }

    IEnumerator WaitForEnd()
    {
        yield return new WaitForSecondsRealtime(timeToMenu);
        characterSelection.SelfDestruction();
        stageSelection.SelfDestruction();
        LoadMainMenu();
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


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TrainingManager : MonoBehaviour
{
    public static TrainingManager instance = null;

    [Header("User Interface")]
    [SerializeField] private Image healthBarP2 = null;
    [SerializeField] private Text ratioTextP2 = null;
    [SerializeField] private Text attackDamageTxt = null;

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

        GetRoundReady();
    }

    void Update()
    {
        UIUpdate();
        ShowAttackDamage();
        SpecialPump();

        if (fighterStatus[1].health != 100)
        {
            StartCoroutine(WaitForHealthReset());
        }
    }

    public void GetRoundReady()
    {
        SetStage();
        InstantiateStage();
        SetPlayers();
        InstantiatePlayers();
        SetFighterStatus();
        SetUI();
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

    public void SetFighterStatus()
    {
        fighterStatus.Add(player1.GetComponent<FighterStatus>());
        fighterStatus.Add(player2.GetComponent<FighterStatus>());
    }

    public void SetUI()
    {
        healthBarP2 = GameObject.FindGameObjectWithTag("HealthBarP2").GetComponent<Image>();
        ratioTextP2 = GameObject.FindGameObjectWithTag("RatioTextP2").GetComponent<Text>();
        attackDamageTxt = GameObject.FindGameObjectWithTag("AttackDamageTxt").GetComponent<Text>();
    }

    public void UIUpdate()
    { 
        healthBarP2.fillAmount = fighterStatus[1].health / 100;
        ratioTextP2.text = fighterStatus[1].health.ToString("0");
    }

    public void HealthReset()
    {
        fighterStatus[1].health = 100;
    }

    public void SpecialPump()
    {
        fighterStatus[0].specialPoints = 100;
    }

    public void ShowAttackDamage()
    {
        attackDamageTxt.text = ((fighterStatus[1].health - 100) * (-1)).ToString("0");
    }

    IEnumerator WaitForHealthReset()
    {
        yield return new WaitForSecondsRealtime(1);
        HealthReset();
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

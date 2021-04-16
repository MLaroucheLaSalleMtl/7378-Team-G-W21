using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Nicholaos, Eduardo and Hayedeh Worked on this Script

public class CountdownController : MonoBehaviour
{
    [Header("Instances")]
    [SerializeField] private GameManager manager;
    [SerializeField] private CharacterSelection characterSelection;

    [Header("Start Screen Manager")]
    [SerializeField] private float timeFightersImagesAreActivated = 1.6f;
    [SerializeField] private float timeBetweenCountdownTxt = 1.0f;
    [SerializeField] private float timeForPlayerOneText = 4.0f;
    [SerializeField] private Text countdownText;
    [SerializeField] private Text playerOneInstruct;
    [SerializeField] private GameObject fightersImages;
    [SerializeField] private GameObject countdown;
    [SerializeField] private GameObject playerInstruct;
    [SerializeField] private GameObject countdownPanel;
    [SerializeField] private GameObject[] player1Images;
    [SerializeField] private GameObject[] player2Images;


    void Start()
    {
        manager = GameManager.instance;
        characterSelection = CharacterSelection.instance;

        if(manager.roundCounterP1 == 0 && manager.roundCounterP2 == 0)
        {
            OpenPanel();
            StartCoroutine(CountdownCoroutine());
        }
        else
        {
            OpenPanel();
            Debug.Log("Round Start after first round");
            StartCoroutine(RoundStartCoroutine());
        }
    }

    IEnumerator CountdownCoroutine()
    {
        yield return new WaitForSecondsRealtime(timeFightersImagesAreActivated);
        FighterImagesDisplayOff();
        PlayerInstructDisplay();
        playerOneInstruct.text = "Player One Press Any Key";
        yield return new WaitForSecondsRealtime(timeForPlayerOneText);
        PlayerInstructDisplay();
        CountdownDisplayOn();
        countdownText.text = "3";
        yield return new WaitForSecondsRealtime(timeBetweenCountdownTxt);
        countdownText.text = "2";
        yield return new WaitForSecondsRealtime(timeBetweenCountdownTxt);
        countdownText.text = "1";
        yield return new WaitForSecondsRealtime(timeBetweenCountdownTxt);
        countdownText.text = "Fight!";
        yield return new WaitForSecondsRealtime(timeBetweenCountdownTxt);
        ClosePanel();

        SoundManager.instance.ChangeMusic("Free Sci-Fi Track 3 (Loop)");
        yield return null;
    }

    IEnumerator RoundStartCoroutine()
    {
        FighterImagesDisplayOff();
        PlayerInstructDisplay();
        playerOneInstruct.text = "Player One Press AnyKey";
        yield return new WaitForSecondsRealtime(timeForPlayerOneText);
        PlayerInstructDisplay();
        ClosePanel();
        yield return null;
    }

    public void OpenPanel()
    {
        Time.timeScale = 0f;
        countdownPanel.SetActive(true);
        GetPlayerImages();
    }

    public void ClosePanel()
    {
        countdownPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void FighterImagesDisplayOff()
    {
        fightersImages.SetActive(false);

        for (int i = 0; i < player1Images.Length; i++)
        {
            player1Images[i].SetActive(false);
        }
        for (int i = 0; i < player2Images.Length; i++)
        {
            player2Images[i].SetActive(false);
        }
    }

    public void CountdownDisplayOn()
    {
        countdown.SetActive(true);
    }

    public void PlayerInstructDisplay()
    {
        if (playerInstruct.activeSelf)
        {
            playerInstruct.SetActive(false);
        }
        else
        {
            playerInstruct.SetActive(true);
        }
    }

    public void GetPlayerImages()
    {
        player1Images[characterSelection.charactersSelected[0].GetComponent<FighterStatus>().playerID].SetActive(true);
        player2Images[characterSelection.charactersSelected[1].GetComponent<FighterStatus>().playerID].SetActive(true);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownController : MonoBehaviour
{
    [Header("Instances")]
    [SerializeField] private GameManager manager;
    [SerializeField] private CharacterSelection characterSelection;

    [Header("Start Screen Manager")]
    [SerializeField] private float timeFightersImagesAreActivated = 1.6f;
    [SerializeField] private float timeBetweenCountdownTxt = 1.0f;
    [SerializeField] private Text countdownText;
    [SerializeField] private GameObject fightersImages;
    [SerializeField] private GameObject countdown;
    [SerializeField] private GameObject countdownPanel;
    [SerializeField] private GameObject[] player1Images;
    [SerializeField] private GameObject[] player2Images;


    void Start()
    {
        manager = GameManager.instance;
        characterSelection = CharacterSelection.instance;

        OpenPanel();
        StartCoroutine(CountdownCoroutine());
    }

    IEnumerator CountdownCoroutine()
    {
        yield return new WaitForSecondsRealtime(timeFightersImagesAreActivated);
        FighterImagesDisplayOff();
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

    public void FighterImagesDisplayOn()
    {
        fightersImages.SetActive(true);
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

    public void CountdownDisplayOff()
    {
        countdown.SetActive(false);
    }

    public void GetPlayerImages()
    {
        player1Images[characterSelection.charactersSelected[0].GetComponent<FighterStatus>().playerID].SetActive(true);
        player2Images[characterSelection.charactersSelected[1].GetComponent<FighterStatus>().playerID].SetActive(true);
    }
}
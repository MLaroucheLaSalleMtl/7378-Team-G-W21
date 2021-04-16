using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Eduardo Worked on this Script

public class SelectCharacters : MonoBehaviour
{
    public GameObject[] Characters;

    public int numberCharacter = 0;

    public Text txtShow;

    void Start()
    {
        numberCharacter = PlayerPrefs.GetInt("Characters");
    }

    void Update()
    {
        if (numberCharacter == 0)
        {
            Characters[0].SetActive(true);
            Characters[1].SetActive(false);
            Characters[2].SetActive(false);
        }

        if (numberCharacter == 1)
        {
            Characters[1].SetActive(true);
            Characters[2].SetActive(false);
            Characters[0].SetActive(false);
        }

        if (numberCharacter == 2)
        {
            Characters[2].SetActive(true);
            Characters[1].SetActive(false);
            Characters[0].SetActive(false);
        }
    }

    public void NextCharacter()
    {
        numberCharacter++;

        if (numberCharacter >= 3)
        {
            numberCharacter = 0;
        }
    }

    public void BackCharacter()
    {
        numberCharacter--;

        if (numberCharacter <= -1)
        {
            numberCharacter = 2;
        }
    }

    public void Select_Ch()
    {
        PlayerPrefs.SetInt("Characters", numberCharacter);
        PlayerPrefs.Save();

        txtShow.text = "Selected";
        StartCoroutine(waitShowtxt());
    }

    [System.Obsolete]
    public void GoLevel()
    {
        Application.LoadLevel("level");
    }

    IEnumerator waitShowtxt()
    {
        yield return new WaitForSeconds(2);
        txtShow.text = "";
    }
}

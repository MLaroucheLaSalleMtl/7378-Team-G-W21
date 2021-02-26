using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SelectCharacters : MonoBehaviour
{

    public GameObject[] Characters;

    public int numberCharacter = 0;

    public Text txtShow;

    // Start is called before the first frame update
    void Start()
    {
        numberCharacter = PlayerPrefs.GetInt("Characters");
    }

    // Update is called once per frame
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelection : MonoBehaviour
{
    private string playModeSelected;
    public GameObject[] charactersList;
    public Vector3 playerSpawnPosition = new Vector3(2.56f, 0.07f, -5.76f);
    public GameObject playerTest;
    public List<GameObject> charactersSelected = new List<GameObject>();

    private void Awake()
    {
        DontDestroyOnLoad(gameObject.transform);
    }

    public void OnCharacterSelection(int charSelected)
    {
        /*
        playerTest = charactersList[charSelected];
        GameObject player = Instantiate(charactersList[charSelected], playerSpawnPosition, Quaternion.identity);
        DontDestroyOnLoad(player);
        LoadScene.instance.LoadNextLevel();
        */

        switch (playModeSelected)
        {
            case "Tutorial":
                if(charactersSelected.Count < 1)
                {
                    charactersSelected.Add(charactersList[charSelected]);
                }
                break;
            case "Training":
                if (charactersSelected.Count < 1)
                {
                    charactersSelected.Add(charactersList[charSelected]);
                }
                break;
            case "2P":
                if (charactersSelected.Count < 2)
                {
                    charactersSelected.Add(charactersList[charSelected]);
                }
                break;
            case "1P":
                if (charactersSelected.Count < 1)
                {
                    charactersSelected.Add(charactersList[charSelected]);
                }
                break;
            default:
                break;
        }
    }

    public void OnPlayMode(string playMode)
    {
        playModeSelected = playMode;
    }

    public List<GameObject> GetCharacters()
    {
        return charactersSelected;
    }

    public string GetPlayMode()
    {
        return playModeSelected;
    }

    public void ClearCharacterSelection()
    {
        charactersSelected.Clear();
    }
}

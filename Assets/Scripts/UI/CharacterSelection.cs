using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelection : MonoBehaviour
{
    public static CharacterSelection instance = null;

    private string playModeSelected;
    public GameObject[] charactersLeftList;
    public GameObject[] charactersRightList;
    public List<GameObject> charactersSelected = new List<GameObject>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject.transform);
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void OnCharacterSelection(int charSelected)
    {
        switch (playModeSelected)
        {
            case "Tutorial":
                if(charactersSelected.Count == 0)
                {
                    charactersSelected.Add(charactersLeftList[charSelected]);
                }
                break;
            case "Training":
                if (charactersSelected.Count == 0)
                {
                    charactersSelected.Add(charactersLeftList[charSelected]);
                }
                break;
            case "2P":
                if (charactersSelected.Count == 0)
                {
                    charactersSelected.Add(charactersLeftList[charSelected]);
                }
                else if (charactersSelected.Count == 1)
                {
                    charactersSelected.Add(charactersRightList[charSelected]);
                }
                break;
            case "1P":
                if (charactersSelected.Count == 0)
                {
                    charactersSelected.Add(charactersLeftList[charSelected]);
                }
                break;
            default:
                break;
        }
    }

    public void OnPlayMode(string playMode)
    {
        ClearCharacterSelection();
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

    public void OnPlayModeSelection()
    {
        LoadScene.instance.LoadAnyScene(playModeSelected);
    }
}

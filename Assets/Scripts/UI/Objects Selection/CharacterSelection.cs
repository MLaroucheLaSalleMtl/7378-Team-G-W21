using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterSelection : MonoBehaviour
{
    public static CharacterSelection instance = null;
    
    [Header("Player Manager")]
    [SerializeField] private GameObject[] charactersLeftList;
    [SerializeField] private GameObject[] charactersRightList;
    public List<GameObject> charactersSelected = new List<GameObject>();
    private string playModeSelected;

    [Header("UI Manager")]
    [SerializeField] private Image[] selectedPlayersLeftImages;
    [SerializeField] private Image[] selectedPlayersRightImages;

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

    public void OnCharacterSelection(int charID)
    {
        if (charactersSelected.Count == 0)
        {
            charactersSelected.Add(charactersLeftList[charID]);
            selectedPlayersLeftImages[charID].GetComponent<Image>().color = Color.yellow;
        }
        else if (charactersSelected.Count == 1)
        {
            charactersSelected.Add(charactersRightList[charID]);
            selectedPlayersRightImages[charID].GetComponent<Image>().color = Color.yellow;
        }
    }

    public void OnPlayMode(string playMode)
    {
        OnClearCharacterSelection();
        StageSelection.instance.OnClearStageSelection();
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

    public void OnConfirmation()
    {
        if (charactersSelected.Count == 2)
        {
            LoadScene.instance.LoadAnyScene(playModeSelected);
        }
    }
    
    public void OnClearCharacterSelection()
    {
        charactersSelected.Clear();
        ClearSelectedPlayersImages();
    }

    public void ClearSelectedPlayersImages()
    {
        for(int i = 0; i < selectedPlayersLeftImages.Length; i++)
        {
            selectedPlayersLeftImages[i].GetComponent<Image>().color = new Color(255, 255, 255, 255);
        }

        for (int i = 0; i < selectedPlayersRightImages.Length; i++)
        {
            selectedPlayersRightImages[i].GetComponent<Image>().color = new Color(255, 255, 255, 255);
        }
    }

    public void SelfDestruction()
    {
        Destroy(gameObject);
    }
}

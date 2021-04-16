using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Eduardo Worked on this Script

public class StageSelection : MonoBehaviour
{
    public static StageSelection instance = null;
    [SerializeField] private SelectMenu selectMenu;

    [Header("Player Manager")]
    [SerializeField] private GameObject[] stagesList;
    public GameObject stageSelected = null;

    [Header("UI Manager")]
    [SerializeField] private Image[] selectedStagesImages;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject.transform);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        selectMenu = SelectMenu.instance;
    }

    public void OnStageSelection(int stageID)
    {
        if(stageSelected == null)
        {
            stageSelected = stagesList[stageID];
            selectedStagesImages[stageID].GetComponent<Image>().color = Color.yellow;
        }
    }

    public GameObject GetStage()
    {
        return stageSelected;
    }

    public void OnConfirmation()
    {
        if(stageSelected != null)
        {
            selectMenu.PanelToggle(4);
        }
    }

    public void OnClearStageSelection()
    {
        stageSelected = null;
        ClearSelectedStagesImages();
    }

    public void ClearSelectedStagesImages()
    {
        for (int i = 0; i < selectedStagesImages.Length; i++)
        {
            selectedStagesImages[i].GetComponent<Image>().color = new Color(255, 255, 255, 255);
        }
    }

    public void SelfDestruction()
    {
        Destroy(gameObject);
    }
}

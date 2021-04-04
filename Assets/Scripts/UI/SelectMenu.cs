using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectMenu : MonoBehaviour
{
    public static SelectMenu instance = null;

    [Header("First item in these list will be selected by default")]
    [SerializeField] private GameObject[] panels = null; // list of all the panels 
    [SerializeField] private Selectable[] defaultBtn = null; // not just for buttons 

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

    IEnumerator Start()
    {
        yield return new WaitForFixedUpdate(); // delay the excecution of the code to the next fixed update
        PanelToggle(0);
    }

    public void SavePrefs()
    {
        PlayerPrefs.Save(); // save the prefrence on disc
    }

    public void PanelToggle(int position) // will enable one panel and its default button
    {
        for (int i = 0; i < panels.Length ; i++) // loop for every panel in the list 
        {
            // will enable or disable the panel 
            panels[i].SetActive(position == i); // position == i : true or false 
            if (position == i)  
            {
                defaultBtn[i].Select(); // will set the focus on the button 
            }
        }
    }
}

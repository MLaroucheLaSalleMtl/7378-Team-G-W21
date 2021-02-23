using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SetGFX : MonoBehaviour
{

    private Dropdown dropdown; // refrence to thje dropdown 
    private string[] _GFXNames; // list of all the graphic settings names  

    // Start is called before the first frame update
    void Start()
    {
        dropdown = GetComponent<Dropdown>(); // cache the dropdown 
        _GFXNames = QualitySettings.names; // cache all the quality setting pr3esets
        List<string> dropOptions = new List<string>(); // list of all the options in the quatlity settings 
        foreach (string str in _GFXNames)
        {
            dropOptions.Add(str);
        }
        dropdown.AddOptions(dropOptions); // populate the new optiuons in the dropdown 
        dropdown.value = QualitySettings.GetQualityLevel(); // retrive the current quality settings 
    }

    public void SetGfx()
    {
        QualitySettings.SetQualityLevel(dropdown.value, true);
    }

}

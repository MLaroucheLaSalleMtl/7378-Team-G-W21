using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//Nicholaos on this script


public class SetResolution : MonoBehaviour
{
    private Dropdown dropdown; // refrence to the current dropdown 
    private Resolution[] resolutions; // array of different supported resolutions 

    // Start is called before the first frame update
    void Start()
    {
        dropdown = GetComponent<Dropdown>(); // cache the dropdown component 
        resolutions = Screen.resolutions; // fill the array with resolutions 
        List<string> dropdownOptions = new List<string>(); // list for the dropdown
        int pos = 0; // position of the current resolution in the list
        int i = 0; // counter for the conversion loops
        Resolution currentRes = Screen.currentResolution; // cache the current resolution
        foreach (Resolution res in resolutions) 
        {
            string s = res.ToString(); // covert the current resolution into a string 
            dropdownOptions.Add(s); // add the current string to the list
            if ((res.width == currentRes.width) 
                && (res.height == currentRes.height) 
                && (res.refreshRate == currentRes.refreshRate)) // if the resolution mathch
            {
                pos = i; // cache the position for later
            }
            i++; // increase the counter
        }
        dropdown.AddOptions(dropdownOptions); // add list of strings to the dropdown 
        dropdown.value = pos; // select the current resolution on the dropdown 
    }

    public void SetRes()
    {
        if (dropdown == null)
        {
            return;
        }

        Resolution res = resolutions[dropdown.value];
        Screen.SetResolution(res.width, res.height, Screen.fullScreenMode, res.refreshRate);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;


//Nicholaos worked on this script



[RequireComponent(typeof(Slider))]
public class SetVol : MonoBehaviour
{
    [SerializeField] private AudioMixer audioM = null; 
    [SerializeField] private string nameParam = null; 
    private Slider slider; 

    void Start()
    {
        slider = GetComponent<Slider>();
        slider.value = PlayerPrefs.GetFloat(nameParam, 0.50f);
    }

    public void SetVolumeTest()
    {
        float sliderVal = slider.value;
        audioM.SetFloat(nameParam, Mathf.Log10(sliderVal) * 20);
        PlayerPrefs.SetFloat(nameParam, sliderVal);
    }

}

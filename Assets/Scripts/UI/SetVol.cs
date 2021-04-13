using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;


[RequireComponent(typeof(Slider))]
public class SetVol : MonoBehaviour
{
    [SerializeField] private AudioMixer audioM = null; 
    [SerializeField] private string nameParam = null; 
    private Slider slider; 

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>(); 
        float v = PlayerPrefs.GetFloat(nameParam, 0);
        Debug.Log(v);
        slider.value = v; 
        audioM.SetFloat(nameParam, v);
    }

    public void SetVolume(float vol) 
    {
        audioM.SetFloat(nameParam, vol); 
        slider.value = vol;
        PlayerPrefs.SetFloat(nameParam, vol); 
    }

}

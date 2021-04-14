using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private List<AudioClip> clips = new List<AudioClip>();
    private Dictionary<string, AudioClip> clipDict = new Dictionary<string, AudioClip>();

    


    private void Start()
    {
        foreach (AudioClip clip in clips)
        {
            Debug.Log(clip.name);
            clipDict.Add(clip.name, clip);
        }
    }

    public AudioClip GetSFX(string clipName)
    {
        AudioClip clip;
        if (clipDict.TryGetValue(clipName, out clip))
        {
            return clip;
        }

        return null;
    }

}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Nicholaos Worked on this Script 

public class SoundManager : MonoBehaviour
{
    [SerializeField] private List<AudioClip> clips = new List<AudioClip>();
    private Dictionary<string, AudioClip> clipDict = new Dictionary<string, AudioClip>();
    [SerializeField] private AudioSource musicSource;

    public static SoundManager instance = null;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }

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
        AudioClip clip = null;
        if (clipDict.TryGetValue(clipName, out clip))
        {
            return clip;
        }

        return null;
    }

    public void ChangeMusic(string songName)
    {
        AudioClip song = GetSFX(songName);
        if (song != null)
        {
            if (musicSource != null)
            {
                musicSource.Stop();
                musicSource.clip = song;
                musicSource.Play();
            }
        }
    }
}

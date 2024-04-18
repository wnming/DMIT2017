using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public AudioSource[] audioSources;
    private float musicVolume = 1f;

    void Start()
    {
        
    }

    void Update()
    {
        for(int index = 0; index < audioSources.Length; index++)
        {
            audioSources[index].volume = musicVolume;
        }
    }

    public void UpdateVolumn(float volume)
    {
        musicVolume = volume;
    }
}

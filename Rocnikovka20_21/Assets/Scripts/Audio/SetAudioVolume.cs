using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetAudioVolume : MonoBehaviour
{
    public List<AudioSource> audioSources = new List<AudioSource>();

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < audioSources.Count; i++)
		{
            audioSources[i].volume = audioSources[i].volume * Settings.volume;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

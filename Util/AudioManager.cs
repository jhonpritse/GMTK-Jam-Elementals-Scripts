using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public Sound[] sounds;
    void Awake()
    {
        foreach (var s in sounds) 
        {
          s.source =  gameObject.AddComponent<AudioSource>();

          s.source.clip = s.clip;
          s.source.volume = s.volume;
          s.source.pitch = s.pitch;
          s.source.loop = s.loop;
        }
    }

    
   public void Play(string clipName)
   {
        Sound s = Array.Find(sounds, sound => sound.name == clipName);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + clipName + " not found!");
            return;
        }
        s.source.Play();
   }

   private void Start()
   {
       Play("Theme");
   }
}

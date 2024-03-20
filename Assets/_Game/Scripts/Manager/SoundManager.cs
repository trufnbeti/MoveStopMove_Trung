using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.NiceVibrations;
using UnityEngine;
using UnityEngine.Serialization;

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField] private Sound[] audioSources;
    [SerializeField] private AudioSource audioSource;

    private Dictionary<SoundType, Sound> sounds = new Dictionary<SoundType, Sound>();

    #region Sound

    public void Play(SoundType type) {
        if (!DataManager.Ins.IsSound)  return;
        
        if (!IsLoaded(type)) {
            sounds.Add(type, GetSound(type));
        }

        audioSource.clip = sounds[type].clip;
        audioSource.volume = sounds[type].volume;
        audioSource.PlayOneShot(audioSource.clip);
        
    }
    
    public void StopAllEfxSound() {
        audioSource.Stop();
    }

    public void SoundClick() {
        Play(SoundType.Click);
        VibrationsManager.instance.TriggerLightImpact();
    }

    #endregion
    
    public bool IsLoaded(SoundType type) {
        return sounds.ContainsKey(type);
    }

    public Sound GetSound(SoundType type) {
        for (int i = 0; i < audioSources.Length; ++i) {
            if (audioSources[i].type == type) {
                return audioSources[i];
            }
        }

        return null;
    }
}

[Serializable]
public class Sound {
    public SoundType type;
    public AudioClip clip;
    [Range(0, 1)]
    public float volume = 1;
}


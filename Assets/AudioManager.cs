using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set;}
    [SerializeField] AudioInfo[] sounds;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }

        foreach (AudioInfo soundInfo in sounds) {
            soundInfo.audioSource = gameObject.AddComponent<AudioSource>();
            soundInfo.audioSource.clip = soundInfo.audioClip;
            soundInfo.audioSource.volume = soundInfo.volume;
            soundInfo.audioSource.loop = soundInfo.loop;
        }
    }

    private void Start() {
        Instance.Play(AudioType.MAIN_MENU);
    }

    public void Play(AudioType audioType) {
        AudioInfo soundInfo = Array.Find(sounds, item => item.audioType == audioType);
        soundInfo.audioSource.Play();
    }

    public void Stop(AudioType audioType) {
        AudioInfo soundInfo = Array.Find(sounds, item => item.audioType == audioType);
        soundInfo.audioSource.Stop();
    }
}

[System.Serializable]
public class AudioInfo {
    public AudioType audioType;
    public AudioClip audioClip;

    [Range(0f, 1f)]
    public float volume;
    public bool loop;

    [HideInInspector]
    public AudioSource audioSource;
}

public enum AudioType {
    MAIN_MENU,
    LEVEL,
    BUTTON_CLICK,
    GAME_OVER,
    GAME_START,
    FOOD_PICKUP,
    POWER_PICKUP,
    MOVEMENT
}
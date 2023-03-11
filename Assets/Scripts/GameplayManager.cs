using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class GameplayManager : MonoBehaviour
{
    public bool isTwoPlayer = false;
    public static GameplayManager Instance { get; private set;}
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
        Instance.PlayAudio(AudioType.MAIN_MENU);
    }

    private void Update() {
        SceneType sceneType = (SceneType) SceneManager.GetActiveScene().buildIndex;
        if (sceneType == SceneType.TWO_PLAYER)
            isTwoPlayer = true;
    }

    public void PlayAudio(AudioType audioType) {
        AudioInfo soundInfo = Array.Find(sounds, item => item.audioType == audioType);
        soundInfo.audioSource.Play();
    }

    public void StopAudio(AudioType audioType) {
        AudioInfo soundInfo = Array.Find(sounds, item => item.audioType == audioType);
        soundInfo.audioSource.Stop();
    }

}


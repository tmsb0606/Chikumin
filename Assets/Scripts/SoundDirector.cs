using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundDirector : MonoBehaviour
{
    public AudioSource bgmSource;
    public AudioSource seSource;
    public static SoundDirector instance;

    private AudioClip _titleBGM;
    private AudioClip _gameBGM;
    private void Awake()
    {
        
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

    }
    private void Start()
    {
        _gameBGM = Resources.Load<AudioClip>("BGM/oriental-drift");
        //bgmSource.clip = _gameBGM;
        SceneManager.activeSceneChanged += OnSceneChange;
    }
    void OnSceneChange(Scene prevScene, Scene nextScene)
    {
        if(nextScene.name == "GameScene")
        {
            print("ƒ`ƒFƒ“ƒW");
            bgmSource.clip = _gameBGM;
            bgmSource.Play();
        }
        if(nextScene.name == "")
        {
            bgmSource.clip = _titleBGM;
            bgmSource.Play();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
        _titleBGM = Resources.Load<AudioClip>("BGM/MusMus-BGM-162");

        string sceneName = SceneManager.GetActiveScene().name;

        if(sceneName == "TitleScene")
        {
            bgmSource.clip = _titleBGM;
        }
        else if(sceneName == "GameScene")
        {
            bgmSource.clip = _gameBGM;
        }
        
        bgmSource.Play();
        //bgmSource.clip = _gameBGM;
        SceneManager.activeSceneChanged += OnSceneChange;
    }
    void OnSceneChange(Scene prevScene, Scene nextScene)
    {
        bgmSource.Stop();
        if(nextScene.name == "GameScene")
        {
            print("ƒ`ƒFƒ“ƒW");
            bgmSource.clip = _gameBGM;
            bgmSource.Play();
        }
        if(nextScene.name == "TitleScene")
        {
            bgmSource.clip = _titleBGM;
            bgmSource.Play();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneDirector : MonoBehaviour
{

    /// <summary>
    ///タイトルでのみ必要 
    /// </summary>

    [SerializeField] private GameObject _loadingUI;
    [SerializeField] private Slider _slider;

    public GameObject FadeObj;
    public void ChangeScene(string name)
    {
        //演出を入れる。
        SceneManager.LoadScene(name);
    }

    public void TitleToGame()
    {
        _loadingUI.SetActive(true);
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(0.1f);
        AsyncOperation async = SceneManager.LoadSceneAsync("GameScene");
        async.allowSceneActivation = false;
        while (async.progress < 0.9f)
        {
            _slider.value = async.progress;
            print(async.progress);
            yield return null;
        }
        _slider.value = 1;

        async.allowSceneActivation = true;
    }

    public void FadeChangeScene(string name)
    {
        //GameObject canvas = GameObject.Find("Canvas");
        Instantiate(FadeObj);
        StartCoroutine(ChangeScene());

    }

    IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(1f);
        AsyncOperation async = SceneManager.LoadSceneAsync("GameScene");
        async.allowSceneActivation = false;
        while (async.progress < 0.9f)
        {
            print(async.progress);
            yield return null;
        }

        async.allowSceneActivation = true;


    }
}

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
}

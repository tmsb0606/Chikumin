using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneDirector : MonoBehaviour
{
    public void ChangeScene(string name)
    {
        //演出を入れる。
        SceneManager.LoadScene(name);
    }
}

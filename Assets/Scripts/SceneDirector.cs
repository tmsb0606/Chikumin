using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneDirector : MonoBehaviour
{
    public void ChangeScene(string name)
    {
        //���o������B
        SceneManager.LoadScene(name);
    }
}

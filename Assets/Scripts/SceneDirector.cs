using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneDirector : MonoBehaviour
{
    public void ChangeScene(string name)
    {
        //‰‰o‚ğ“ü‚ê‚éB
        SceneManager.LoadScene(name);
    }
}

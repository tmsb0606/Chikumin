using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleDirector : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject camera;
    public GameObject fade;
    void Start()
    {
        Time.timeScale = 1;

    }

    // Update is called once per frame
    void Update()
    {
        camera.transform.Rotate(0,0.1f,0);
    }

    public void FadeEnd()
    {
        fade.SetActive(false);
    }
}

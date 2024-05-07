using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelController : MonoBehaviour
{
    [SerializeField] private GameObject _panel;

    public void OpenPanel()
    {
        //何かアニメーションを入れる。
        _panel.SetActive(true);
    }
    public void ClosePanel()
    {
        _panel.SetActive(false);
    }
}

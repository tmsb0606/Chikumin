using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelController : MonoBehaviour
{
    [SerializeField] private GameObject _panel;

    public void OpenPanel()
    {
        //�����A�j���[�V����������B
        _panel.SetActive(true);
    }
    public void ClosePanel()
    {
        _panel.SetActive(false);
    }
}

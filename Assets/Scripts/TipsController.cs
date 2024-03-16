using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TipsController : MonoBehaviour
{
    TipsReader tips;
    public TextMeshProUGUI nametext;
    public TextMeshProUGUI tipstext;
    public GameObject TipsObj;
    // Start is called before the first frame update
    void Start()
    {

        /*        tips = this.GetComponent<TipsReader>();
                int num = Random.Range(0, tips.csvDatas[0][0].Length);
                nametext.text = tips.csvDatas[num][1];
                tipstext.text = tips.csvDatas[num][2];*/


        ShowTipsLoadToServer();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowTips()
    {
        tips = this.GetComponent<TipsReader>();
        int num = Random.Range(0, tips.csvDatas[0][0].Length);
        nametext.text = tips.csvDatas[num][1];
        tipstext.text = tips.csvDatas[num][2];
    }

    public void ShowTipsLoadToServer()
    {
        tips = this.GetComponent<TipsReader>();
        int num = Random.Range(0, TipsData.csvDatas[0][0].Length);
        nametext.text = TipsData.csvDatas[num][1];
        tipstext.text = TipsData.csvDatas[num][2];
    }
}

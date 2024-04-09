using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreAnimation : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI score;
    private float time;
    private float easingTime = 1.0f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        ScoreAnim();
    }

    public void ScoreAnim()
    {
        
        //tが進行度、totaltimeが目標の時間、minが開始値、maxが目標値
        score.fontSize = Easing.ElasticOut(time, 2f, 100, 130);
    }

}

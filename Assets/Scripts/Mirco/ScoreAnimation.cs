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
        
        //t���i�s�x�Atotaltime���ڕW�̎��ԁAmin���J�n�l�Amax���ڕW�l
        score.fontSize = Easing.ElasticOut(time, 2f, 100, 130);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreUpAnim : MonoBehaviour
{
    [Header("TweenOption")]
    [SerializeField] private float _tweenTime;
    [SerializeField] private float _fadeOutTime;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _delay;

    //[SerializeField] private float maxScale;


    [Header("Reference")]
    [SerializeField] private TextMeshProUGUI textMesh;

    [Header("TextColor")]
    private Color color;
    private Color fadeoutColor;

    
    


    // Start is called before the first frame update
    void Start()
    {
        //透明化する際に必要な情報たち
        textMesh = this.gameObject.GetComponent<TextMeshProUGUI>();
        color = textMesh.color;
        fadeoutColor = color;
        fadeoutColor.a = 0;

    }

    // Update is called once per frame
    void Update()
    {
        
       
    }

    //テスト用アニメーション
    /*public void Tween()
    {
        LeanTween.cancel(this.gameObject);

        transform.localScale = Vector3.one;

        LeanTween.scale(this.gameObject, Vector3.one * _maxScale, _tweenTime).setEaseOutExpo();

        LeanTween.value(this.gameObject, 0, (int)1000000, _tweenTime).setEaseOutSine().setOnUpdate(setText).setOnComplete(() =>
        {
            LeanTween.scale(this.gameObject, Vector3.one, _tweenTime).setEaseOutElastic();
        });

    }*/
    

    public void TakeScoreAnim()
    {
        Vector3 targetPos = transform.localPosition + _offset;

        LeanTween.moveLocal(this.gameObject, targetPos, _tweenTime).setEase(LeanTweenType.easeOutBack);
        LeanTween.value(this.gameObject, updateValueExampleCallback, color, fadeoutColor, _fadeOutTime).setEase(LeanTweenType.easeOutCirc).setDelay(_delay);
    }

    

    public void setText(float value)
    {
        textMesh.text = value.ToString("");
    }

    void updateValueExampleCallback(Color val)
    {
        textMesh.color = val;
    }
}

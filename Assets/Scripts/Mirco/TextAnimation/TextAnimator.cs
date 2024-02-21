using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteInEditMode]

public class TextAnimator : MonoBehaviour
{
    //アニメーションさせたい言葉をここに入力
    [SerializeField] private string message;

    //アニメーションの長さ
    [SerializeField] private float stringAnimationDuration;

    //アニメーションさせたいTextMeshPro
    [SerializeField] private TextMeshProUGUI animatedText;

    [Header("Text Scale")]
    [SerializeField] private AnimationCurve sizeCurve;
    [SerializeField] private float sizeScale;

    [Header("Height")]
    [SerializeField] private AnimationCurve heightCurve;
    [SerializeField] private float heightScale;

    [Header("Rotation")]
    [SerializeField] private AnimationCurve rotationCurve;
    [SerializeField] private float rotationScale;

    [Header("Color")]
    [SerializeField] private Gradient color;







    [SerializeField] [Range(0.0001f, 1)] private float charAnimationDuration;

    [SerializeField] [Range(0, 1)] private float editorTValue;

    private float timeElapsed;

    public IEnumerator RunAnimation(float waitForSeconds)
    {
        yield return new WaitForSeconds(waitForSeconds);
        float t = 0;
        timeElapsed = 0f;
        while(t <= 1f)
        {
            EvaluateRichText(t);
            t = timeElapsed / stringAnimationDuration;
            timeElapsed += Time.deltaTime;

            yield return null;
        }

    }

    private void Start()
    {
        StartCoroutine(RunAnimation(1));
    }

    private void Update()
    {
        EvaluateRichText(editorTValue);
    }


    public void EvaluateRichText(float t)
    {
        animatedText.text = "";

        for (int i = 0; i < message.Length; i++)
        {
            animatedText.text += EvaluateCharRichText(message[i], message.Length, i, t);
        }
    }

    
    private string EvaluateCharRichText(char c, int sLength, int cPosition, float t)  //(どの文字に注目するか, 文字列の長さ, 文字列内の文字の位置, アニメーションがどこまで進んでいるか)
    {
        
        //アニメーションの始まる場所を取得
        float startPoint = ((1 - charAnimationDuration) / (sLength - 1)) * cPosition;

        //アニメーションの終了点の取得
        float endPoint = startPoint + charAnimationDuration;

        float subT = t.Map(startPoint, endPoint, 0, 1);


        //TextMeshProの中にタグを書き込む処理
        string sizeStart = $"<size={sizeCurve.Evaluate(subT) * sizeScale}%>";
        string sizeEnd = "</size>";

        string vOffsetStart = $"<voffset={heightCurve.Evaluate(subT) * heightScale}px>";
        string vOffsetEnd = "</voffset>";

        string rotateStart = $"<rotate={rotationCurve.Evaluate(subT) * rotationScale}%>";
        string rotateEnd = "</rotate>";

        //string colorStart = $"<color=#{ColorUtility.ToHtmlStringRGBA(color.Evaluate(subT))}>";
        //string colorEnd = "</color>";

        return sizeStart +vOffsetStart + rotateStart + c + rotateEnd + vOffsetEnd + sizeEnd;
    }

    public void setMessage(string str)
    {
        if(message != str)
        {
            message = str;

            StartCoroutine(RunAnimation(0));
        }
    }
    
}

//拡張機能：既存のクラスに機能を追加できる
public static class Extensions
{
    public static float Map(this float value, float fromLow, float fromHigh, float toLow, float toHigh)
    {
        return (value - fromLow) * (toHigh - toLow) / (fromHigh - fromLow) - toLow;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultAnim : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] private CanvasGroup itemLine;

    [Header("AnimationSetting")]
    [SerializeField] private float fadeTime;
    [SerializeField] private float scaleTime;



    public void ItemLineAnim()
    {
       
        LeanTween.value(0f, 1f, fadeTime).setOnUpdate(UpdateitemLineAlpha);
        LeanTween.scale(this.gameObject, Vector3.one, scaleTime).setEaseOutBack();

    }

    void UpdateitemLineAlpha(float value)
    {
        itemLine.alpha = value;
    }

    
    
}

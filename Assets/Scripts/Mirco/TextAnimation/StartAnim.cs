using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StartAnim : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] private TextMeshProUGUI textMesh;


    [Header("AnimationOption")]
    [SerializeField] private float fadeInTime;
    [SerializeField] private float scaleTime;
    [Space(10)]
    [SerializeField] private float bounceScale;
    [SerializeField] private float bounceTime;
    [SerializeField] private float moveYOffset;
    [SerializeField] private float bounceDelay;
    [Space(10)]
    [SerializeField] private float fadeOutTime;
    [SerializeField] private float fadeOutDelay;

    [Header("TextColor")]
    private Color color;
    private Color fadeOutColor;
    private Color fadeInColor;
    private float alpha;

    // Start is called before the first frame update
    void Start()
    {
        //ìßñæâªÇ∑ÇÈç€Ç…ïKóvÇ»èÓïÒ
        textMesh = this.gameObject.GetComponent<TextMeshProUGUI>();
        color = textMesh.color;

        fadeOutColor = color;
        fadeInColor = color;

        fadeOutColor.a = 0;
        fadeInColor.a = 1;

    }

    public void FinishTextAnim()
    {
        LeanTween.cancel(this.gameObject);
        LeanTween.value(this.gameObject, updateValueExampleCallback, color, fadeInColor, fadeInTime).setEase(LeanTweenType.easeOutCirc);
        LeanTween.scale(this.gameObject, Vector3.one, scaleTime).setEaseOutBack().setOnComplete(() =>
        {
            LeanTween.scaleY(this.gameObject, bounceScale, bounceTime).setEaseOutExpo().setDelay(bounceDelay);
            LeanTween.moveLocalY(this.gameObject, moveYOffset, bounceTime).setEaseInOutBack().setDelay(bounceDelay);
            LeanTween.scaleY(this.gameObject, 1.1f, 0.5f).setEaseOutExpo().setDelay(bounceDelay).setDelay(bounceDelay + 0.5f);
            LeanTween.value(this.gameObject, updateValueExampleCallback, fadeInColor, fadeOutColor, fadeOutTime).setEase(LeanTweenType.easeOutCirc).setDelay(fadeOutDelay);
        });
    }

    void updateValueExampleCallback(Color val)
    {
        textMesh.color = val;
    }
}

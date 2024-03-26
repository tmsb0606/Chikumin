using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Threading.Tasks;
using System.Threading;

public class FinishAnim : MonoBehaviour
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
        //透明化する際に必要な情報
        textMesh = this.gameObject.GetComponent<TextMeshProUGUI>();
        color = textMesh.color;

        fadeOutColor =  color;
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

    public async Task AsyncFinishTextAnim(CancellationToken cancellationToken = default)
    {
        LeanTween.cancel(this.gameObject);
        LeanTween.value(this.gameObject, updateValueExampleCallback, color, fadeInColor, fadeInTime).setEase(LeanTweenType.easeOutCirc);
        /*        var tween =  LeanTween.scale(this.gameObject, Vector3.one, scaleTime).setEaseOutBack().setOnComplete(() =>
                {
                    LeanTween.scaleY(this.gameObject, bounceScale, bounceTime).setEaseOutExpo().setDelay(bounceDelay);
                    LeanTween.moveLocalY(this.gameObject, moveYOffset, bounceTime).setEaseInOutBack().setDelay(bounceDelay);
                    LeanTween.scaleY(this.gameObject, 1.1f, 0.5f).setEaseOutExpo().setDelay(bounceDelay).setDelay(bounceDelay + 0.5f);
                    LeanTween.value(this.gameObject, updateValueExampleCallback, fadeInColor, fadeOutColor, fadeOutTime).setEase(LeanTweenType.easeOutCirc).setDelay(fadeOutDelay);
                });*/

        //await tween.AwaitCompletionAsync(cancellationToken);

        LeanTween.scale(this.gameObject, Vector3.one, scaleTime).setEaseOutBack().setOnComplete(() =>
        {
            LeanTween.scaleY(this.gameObject, bounceScale, bounceTime).setEaseOutExpo().setDelay(bounceDelay);
            LeanTween.moveLocalY(this.gameObject, moveYOffset, bounceTime).setEaseInOutBack().setDelay(bounceDelay);
            LeanTween.scaleY(this.gameObject, 1.1f, 0.5f).setEaseOutExpo().setDelay(bounceDelay).setDelay(bounceDelay + 0.5f);
            LeanTween.value(this.gameObject, updateValueExampleCallback, fadeInColor, fadeOutColor, fadeOutTime).setEase(LeanTweenType.easeOutCirc).setDelay(fadeOutDelay);
        });
        await Task.Delay(2900);



    }


    void updateValueExampleCallback(Color val)
    {
        textMesh.color = val;
    }
}

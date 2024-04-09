using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System.Threading;
using Cysharp.Threading.Tasks;

public class ResultAnim : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] private CanvasGroup itemLine;

    [Header("AnimationSetting")]
    [SerializeField] private float fadeTime;
    [SerializeField] private float scaleTime;

    private void Awake()
    {
        itemLine = this.GetComponent<CanvasGroup>();
    }


    public void ItemLineAnim()
    {
       
        LeanTween.value(0f, 1f, fadeTime).setOnUpdate(UpdateitemLineAlpha);
        LeanTween.scale(this.gameObject, Vector3.one, scaleTime).setEaseOutBack();

    }

    public async Task AsyncItemAnim(CancellationToken cancellationToken = default)
    {
        LeanTween.value(0f, 1f, fadeTime).setOnUpdate(UpdateitemLineAlpha);
        /*        var tween =  LeanTween.scale(this.gameObject, Vector3.one, scaleTime).setEaseOutBack();
                await tween.AwaitCompletionAsync(cancellationToken);*/

        LeanTween.scale(this.gameObject, Vector3.one, scaleTime).setEaseOutBack();
        await UniTask.Delay(500);
    }

    void UpdateitemLineAlpha(float value)
    {
        itemLine.alpha = value;
    }

    
    
}

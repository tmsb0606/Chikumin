using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TweenText : MonoBehaviour
{
    [SerializeField] float tweenTime;
    [SerializeField] float maxScale;
    [SerializeField] TextMeshProUGUI textMesh;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Tween()
    {
        LeanTween.cancel(this.gameObject);

        transform.localScale = Vector3.one;

        LeanTween.scale(this.gameObject, Vector3.one * maxScale, tweenTime).setEaseOutExpo();

        LeanTween.value(this.gameObject, 0, (int)1000000, tweenTime).setEaseOutSine().setOnUpdate(setText).setOnComplete(() =>
        {
            LeanTween.scale(this.gameObject, Vector3.one, tweenTime).setEaseOutElastic();
        });

            


    }

    public void setText(float value)
    {
        textMesh.text = value.ToString("");
    }
}

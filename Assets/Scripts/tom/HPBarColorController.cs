using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBarColorController : MonoBehaviour
{
    private Image _image;
    // Start is called before the first frame update
    void Start()
    {
        _image = this.gameObject.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_image.fillAmount > 0.60)
        {
            _image.color = new Color(0,255,0);
        }
        else if(_image.fillAmount > 0.35)
        {
            _image.color = new Color(255, 255, 0);
        }
        else
        {
            _image.color = new Color(255, 0,0);
        }
    }
}

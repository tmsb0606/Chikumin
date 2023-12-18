using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject _lookObject;
    [SerializeField] private float _dis = 10f;
    [SerializeField] private Quaternion _vRota;
    [SerializeField] public Quaternion _hRota;

    public float prevYRota;
    // Start is called before the first frame update
    void Start()
    {
        _vRota = Quaternion.Euler(30, 0, 0);
        _hRota = Quaternion.identity;
        prevYRota = 0;

        this.transform.rotation = _hRota * _vRota;
        transform.position = _lookObject.transform.position - transform.rotation * Vector3.forward * _dis;
    }

    // Update is called once per frame
    void Update()
    {
        ChangeTransform();


        //å„Ç©ÇÁèëÇ≠ÅBèôÅXÇ…ÉJÉÅÉâà⁄ìÆ
        //prevYRota = Mathf.Lerp(prevYRota,_hRota.y,Time.deltaTime*2f);
        //Quaternion quaternion = new Quaternion(_hRota.x, prevYRota, _hRota.z, _hRota.w);
        //transform.rotation = quaternion * _vRota;
    }

    public void ChangeTransform()
    {
        //print(_lookObject.transform.rotation.y * Mathf.Rad2Deg);
       
        transform.position = _lookObject.transform.position - transform.rotation * Vector3.forward * _dis;
    }
    public void Rotaion()
    {
        prevYRota = this.transform.localEulerAngles.y;
        //print(prevYRota);
        _hRota = Quaternion.Euler(0, _lookObject.transform.localEulerAngles.y, 0);
        transform.rotation = _hRota * _vRota;

    }
}

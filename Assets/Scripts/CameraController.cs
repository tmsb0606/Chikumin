using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject _lookObject;
    [SerializeField] private float _dis = 10f;
    [SerializeField] private Quaternion _vRota;
    [SerializeField] public Quaternion _hRota;
    [SerializeField] private float _rotaSpeed = 0.05f;

    public Quaternion prevYRota;
    // Start is called before the first frame update
    void Start()
    {
        _vRota = Quaternion.Euler(30, 0, 0);
        _hRota = Quaternion.identity;
        prevYRota = _hRota;

        this.transform.rotation = _hRota * _vRota;
        transform.position = _lookObject.transform.position - transform.rotation * Vector3.forward * _dis;
    }

    // Update is called once per frame
    void Update()
    {
        ChangeTransform();


        //å„Ç©ÇÁèëÇ≠ÅBèôÅXÇ…ÉJÉÅÉâà⁄ìÆ
        prevYRota = Quaternion.Lerp(prevYRota,_hRota,_rotaSpeed);
        //prevYRota = _hRota.y;
        //Quaternion quaternion = new Quaternion(_hRota.x, prevYRota.y, _hRota.z, _hRota.w);
        transform.rotation = prevYRota * _vRota;
        print(",_hRota.y" +_hRota.y);
        print("prev"+ prevYRota);
    }

    public void ChangeTransform()
    {
        //print(_lookObject.transform.rotation.y * Mathf.Rad2Deg);
       
        transform.position = _lookObject.transform.position - transform.rotation * Vector3.forward * _dis;
    }
    public void Rotaion()
    {
        prevYRota = _hRota;
        //print(prevYRota);
        _hRota = Quaternion.Euler(0, _lookObject.transform.localEulerAngles.y, 0);
        
        //transform.rotation = _hRota * _vRota;

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController1 : MonoBehaviour
{
    [SerializeField] private GameObject _lookObject;
    [SerializeField] private float _dis = 10f;
    [SerializeField] private Quaternion _vRota;
    [SerializeField] private Quaternion _hRota;
    // Start is called before the first frame update
    void Start()
    {
        _vRota = Quaternion.Euler(30, 0, 0);
        _hRota = Quaternion.identity;
        
        this.transform.rotation = _hRota * _vRota;
        transform.position = _lookObject.transform.position - transform.rotation * Vector3.forward * _dis;
    }

    // Update is called once per frame
    void Update()
    {
        ChangeTransform();
    }

    public void ChangeTransform()
    {
        print(_lookObject.transform.rotation.y * Mathf.Rad2Deg);
        _hRota = Quaternion.Euler(0, _lookObject.transform.localEulerAngles.y, 0);
        transform.rotation = _hRota * _vRota;
        transform.position = _lookObject.transform.position - transform.rotation * Vector3.forward * _dis;
    }
}

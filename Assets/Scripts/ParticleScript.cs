using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleScript : MonoBehaviour
{
    //　ScaleUp用の経過時間
    private float elapsedScaleUpTime = 0f;
    //　Scaleを大きくする間隔時間
    [SerializeField]
    private float scaleUpTime = 0.03f;
    //　ScaleUpする割合
    [SerializeField]
    private float scaleUpParam = 0.1f;
    //　パーティクル削除用の経過時間
    private float elapsedDeleteTime = 0f;
    //　パーティクルを削除するまでの時間
    [SerializeField]
    private float deleteTime = 5f;

    private float speed = 2f;

    private Vector3 limitScale = new Vector3(3, 3, 3);
    private Vector3 downLimitScale = new Vector3(0, 0, 0);

    public bool isScaling = false;

    private Vector3 rotateSpeed = new Vector3(0, 0, -3);

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotateSpeed);

        if (isScaling)
        {
            UpScaling();
        }
        else
        {
            DownScaling();
        }
    }

    private void UpScaling()
    {
        print("パーティクルアップ");
        if(transform.localScale.x < limitScale.x)
        {
            transform.localScale += new Vector3(scaleUpParam, scaleUpParam, scaleUpParam)*speed;
        }
    }
    private void DownScaling()
    {
        print("パーティクルダウン");
        if (transform.localScale.x >= downLimitScale.x)
        {
            transform.localScale -= new Vector3(scaleUpParam, scaleUpParam, scaleUpParam) * speed;
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
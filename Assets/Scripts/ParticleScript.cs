using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleScript : MonoBehaviour
{
    //�@ScaleUp�p�̌o�ߎ���
    private float elapsedScaleUpTime = 0f;
    //�@Scale��傫������Ԋu����
    [SerializeField]
    private float scaleUpTime = 0.03f;
    //�@ScaleUp���銄��
    [SerializeField]
    private float scaleUpParam = 0.1f;
    //�@�p�[�e�B�N���폜�p�̌o�ߎ���
    private float elapsedDeleteTime = 0f;
    //�@�p�[�e�B�N�����폜����܂ł̎���
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
        print("�p�[�e�B�N���A�b�v");
        if(transform.localScale.x < limitScale.x)
        {
            transform.localScale += new Vector3(scaleUpParam, scaleUpParam, scaleUpParam)*speed;
        }
    }
    private void DownScaling()
    {
        print("�p�[�e�B�N���_�E��");
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
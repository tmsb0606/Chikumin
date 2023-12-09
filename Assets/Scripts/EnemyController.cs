using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour, IDamageable
{
    // Start is called before the first frame update
    int hp = 1000;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (hp < 0)
        {
            Death();
        }
    }

    public void Damage(int value)
    {
        // �����ɋ�̓I�ȃ_���[�W����
        hp -= 1;
    }

    public void Death()
    {
        // �����ɋ�̓I�Ȏ��S����
        this.gameObject.SetActive(false);
    }
}

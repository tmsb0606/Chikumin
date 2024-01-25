using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour, IDamageable
{
    // Start is called before the first frame update
    int hp = 100;
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
        // ここに具体的なダメージ処理
        hp -= 1;
    }

    public void Death()
    {
        // ここに具体的な死亡処理
        this.gameObject.SetActive(false);
    }
}

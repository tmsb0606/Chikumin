using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour, IDamageable
{
    // Start is called before the first frame update
    public enum EnemyAiState
    {
        Wait,
        RandomMove,
        Chase,
        Attack,

    }
    NavMeshAgent agent;
    int hp = 100;
    public EnemyAiState aiState = EnemyAiState.RandomMove;

    public GameObject targetObject;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hp < 0)
        {
            Death();
        }
        switch (aiState)
        {
            case EnemyAiState.RandomMove:
                RandomMove();
                break;
            case EnemyAiState.Chase:
                Chase();
                break;
        }
    }
    public void RandomMove()
    {

    }
    public void Chase()
    {
        agent.SetDestination(targetObject.transform.position);
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
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //targetObject = other.gameObject;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChikuminBase : MonoBehaviour
{
    public enum ChikuminAiState
    {
        WAIT,           //�s������U��~
        MOVE,           //�ړ�
        ATTACK,     //��~���čU��
        IDLE,           //�ҋ@
    }
    public ChikuminAiState aiState = ChikuminAiState.MOVE;
    NavMeshAgent agent;
    private GameObject targetPlayer;
    
    // Start is called before the first frame update
    void Start()
    {
        targetPlayer = GameObject.Find("Player");
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {

        switch (aiState)
        {
            case ChikuminAiState.WAIT:
                Wait();
                break;
            case ChikuminAiState.MOVE:
                Move();
                break;

        }
    }

    private void Wait()
    {

    }
    private void Move()
    {
        agent.SetDestination(targetPlayer.transform.position);
    }
}

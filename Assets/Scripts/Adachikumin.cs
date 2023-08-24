using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Adachikumin : ChikuminBase
{
    //public ChikuminAiState aiState = ChikuminAiState.MOVE;
    NavMeshAgent agent;
    private GameObject targetPlayer;
    public CharacterStatus status;
    void Start()
    {
        targetPlayer = GameObject.Find("Player");
        agent = GetComponent<NavMeshAgent>();
        changeStatus();
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
    private void  changeStatus()
    {
        agent.speed = status.moveSpeed;
    }
}

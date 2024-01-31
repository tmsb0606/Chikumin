using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class AlignmentArea : MonoBehaviour
{
    private GameObject targetPlayer;
    NavMeshAgent agent;
    public float moveDis = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        targetPlayer = GameObject.Find("Player");
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    private void Move()
    {
        if (Vector3.Distance(targetPlayer.transform.position, this.transform.position) > moveDis)
        {
            agent.speed = 10;
            agent.SetDestination(targetPlayer.transform.position);
        }
        else
        {
            agent.speed = 0;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChikuminBase : MonoBehaviour
{
    public enum ChikuminAiState
    {
        WAIT,           //s“®‚ğˆê’U’â~
        MOVE,           //ˆÚ“®
        ATTACK,     //’â~‚µ‚ÄUŒ‚
        IDLE,           //‘Ò‹@
        CARRY,          //ƒAƒCƒeƒ€‚ğ‰^‚Ô
        ALIGNMENT,      //‘Ò‹@‚·‚é‚½‚ß‚É®—ñ‚·‚é
        ONRUSH,         //“ËŒ‚‚·‚é
    }
    public ChikuminAiState aiState = ChikuminAiState.MOVE;

    public ChikuminAiState prevState = ChikuminAiState.MOVE;
    NavMeshAgent agent;
    public GameObject targetPlayer;
    public List<GameObject> hitList = new List<GameObject>();
    public List<GameObject> carryObjectList = new List<GameObject>();
    public GameObject waitArea;
    public Vector3 waitPos;
    public bool isHit = false;

    public bool isItem = false;

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

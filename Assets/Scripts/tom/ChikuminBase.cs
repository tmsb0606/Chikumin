using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public partial class ChikuminBase : MonoBehaviour
{

    /// <summary>
    /// ステートパターンのテスト
    /// </summary>
    private static readonly StateWaiting stateWaiting = new StateWaiting();
    private static readonly StateMoving stateMoving = new StateMoving();
    private static readonly StateCarrying stateCarrying = new StateCarrying();
    [SerializeField]private GameObject goalObject;

    [SerializeField] protected AudioClip punchSE;
    [SerializeField] protected AudioClip carrySE;
    protected float carrySeTime;

    protected ChikuminStateBase currentState = stateWaiting;
    public GameObject targetObject;
    private void ChangeState(ChikuminStateBase nextState)
    {
        currentState.OnExit(this, nextState);
        nextState.OnEnter(this, currentState);
        currentState = nextState;
    }
    /// <summary>
    /// ここまで
    /// </summary>




    public enum ChikuminAiState
    {
        WAIT,           //行動を一旦停止
        MOVE,           //移動
        ATTACK,     //停止して攻撃
        IDLE,           //待機
        CARRY,          //アイテムを運ぶ
        ALIGNMENT,      //待機するために整列する
        ONRUSH,         //突撃する
    }
    public ChikuminAiState aiState = ChikuminAiState.MOVE;

    public ChikuminAiState prevState = ChikuminAiState.MOVE;
    public NavMeshAgent agent;
    public GameObject targetPlayer;
    public List<GameObject> hitList = new List<GameObject>();
    public List<GameObject> carryObjectList = new List<GameObject>();
    public GameObject waitArea;
    public Vector3 waitPos;
    public bool isHit = false;

    public bool isItem = false;
    public float _carrySpeed = 5f;

    Animator animator;

    public bool canCall = true;

    // Start is called before the first frame update
    void Start()
    {
        targetPlayer = GameObject.Find("Player");
        agent = GetComponent<NavMeshAgent>();
    }


    private void Wait()
    {

    }
    private void Idle()
    {

    }
    private void Move()
    {
        agent.SetDestination(targetPlayer.transform.position);
    }
    private void Attack()
    {

    }
    private void AttackDamage()
    {

    }
    private void Carry()
    {

    }
    private void Alignment()
    {

    }

    private void OnRush()
    {

    }
    protected GameObject NearObject(List<GameObject> gameObjects)
    {
        GameObject nearObj = gameObjects[0];
        float dis = Vector3.Distance(gameObjects[0].transform.position, this.transform.position);
        foreach (GameObject obj in gameObjects)
        {
            float dis2 = Vector3.Distance(obj.transform.position, this.transform.position);
            if (dis >= dis2)
            {
                dis = dis2;
                nearObj = obj;
            }
        }
        return nearObj;
    }

    private void Death()
    {

    }

}

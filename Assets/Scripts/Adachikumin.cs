using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Adachikumin : ChikuminBase
{
    //public ChikuminAiState aiState = ChikuminAiState.MOVE;
    NavMeshAgent agent;
    private GameObject targetPlayer;
    private GameObject targetObject;
    private GameObject goalObject;
    
    public CharacterStatus status;
    private bool isHit = false;
    private bool isEnemy = false;
    private bool isItem = false;
    private bool isGround = false;

    public float moveDis = 1.0f;

    private AudioSource audioSource;
    public AudioClip throwSE;
    //public List<GameObject> hitList = new List<GameObject>();

    void Start()
    {
        targetPlayer = GameObject.Find("Player");
        goalObject = GameObject.Find("Goal");
        agent = GetComponent<NavMeshAgent>();
        changeStatus();
        audioSource = GameObject.Find("SoundDirector").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        switch (aiState)
        {
            case ChikuminAiState.IDLE:
                Idle();
                break;
            case ChikuminAiState.WAIT:
                Wait();
                break;
            case ChikuminAiState.MOVE:
                Move();
                break;
            case ChikuminAiState.ATTACK:
                Attack();
                break;
            case ChikuminAiState.CARRY:
                Carry();
                break;

        }

    }

    private void Wait()
    {
        agent.speed = 0;
        //print(isGround);
        if (!isGround)
        {
            
            return;
        }
        //今後の動作判定
        if(hitList.Count != 0)
        {
            targetObject = NearObject(hitList);
            //print(targetObject);
            if (targetObject.tag == "enemy")
            {

          
                hitList.Remove(targetObject);
                aiState = ChikuminAiState.ATTACK;
            }
            else if(targetObject.tag == "item")
            {
                hitList.Remove(targetObject);
                aiState = ChikuminAiState.CARRY;
            }
            else if (targetObject.tag == "atm")
            {
                targetObject.GetComponent<ATMController>().ReleaseMoney();
                hitList.Remove(targetObject);
                aiState = ChikuminAiState.WAIT;
            }
        }
        

    }
    private void Idle()
    {
        agent.speed = 0;
    }
    private void Move()
    {
        if (Vector3.Distance(targetPlayer.transform.position, this.transform.position) > moveDis)
        {
            changeStatus();
            agent.SetDestination(targetPlayer.transform.position);
        }
        else
        {
            agent.speed = 0;
        }
    }
    private void Attack()
    {
        //print("attack");
        changeStatus();
        agent.SetDestination(targetObject.transform.position);
        targetObject.gameObject.GetComponent<IDamageable>().Damage(1);
    }
    private void Carry()
    {
        if(carryObject == null)
        {
            carryObject = targetObject.gameObject;
            carryObject.transform.parent = this.transform;
        }
        
        changeStatus();
        agent.SetDestination(goalObject.transform.position);
        //Move();
    }
    private GameObject NearObject(List<GameObject> gameObjects)
    {
        GameObject nearObj = gameObjects[0];
        float dis = Vector3.Distance(gameObjects[0].transform.position, this.transform.position) ;
        foreach(GameObject obj in gameObjects)
        {
            float dis2 = Vector3.Distance(obj.transform.position, this.transform.position);
            if (dis>= dis2)
            {
                dis = dis2;
                nearObj = obj;
            }
        }
        return nearObj;
    }

    private void  changeStatus()
    {
        agent.speed = status.moveSpeed;
    }
    public void OnCollisionEnter(UnityEngine.Collision other)
    {
        this.GetComponent<Rigidbody>().isKinematic = true;
       
       if (other.gameObject.tag != "tiku"&& other.gameObject.tag != "circle")
        {
            //hitList.Add(other.gameObject);
            isHit = true;
            
        }

        if (other.gameObject.tag == "goal")
        {
            //print("goal");
            carryObject.transform.parent = null;
            carryObject.SetActive(false);
            carryObject = null;
        }
    }
    public void OnCollisionStay(UnityEngine.Collision other)
    {
        //print(other.gameObject.tag);
        if (other.gameObject.tag == "ground")
        {
           // isGround = true;
        }
    }
    public void OnCollisionExit(UnityEngine.Collision other)
    {
        //this.GetComponent<Rigidbody>().isKinematic = true;
        if (other.gameObject.tag != "tiku" && other.gameObject.tag != "circle")
        {
            //hitList.Remove(other.gameObject);
            isHit = true;
        }
        if (other.gameObject.tag == "ground")
        {
           // isGround = false;
        }

    }
    public IEnumerator Jump(Vector3 endPos, float flightTime, float speedRate, float gravity)
    {
        changeStatus();
        audioSource.PlayOneShot(throwSE);
        isGround = false;
        var startPos = transform.position; // 初期位置
        var diffY = (endPos - startPos).y; // 始点と終点のy成分の差分
        var vn = (diffY - gravity * 0.5f * flightTime * flightTime) / flightTime; // 鉛直方向の初速度vn

        // 放物運動
        for (var t = 0f; t < flightTime; t += (Time.deltaTime * speedRate))
        {
            var p = Vector3.Lerp(startPos, endPos, t / flightTime);   //水平方向の座標を求める (x,z座標)
            p.y = startPos.y + vn * t + 0.5f * gravity * t * t; // 鉛直方向の座標 y
            transform.position = p;
            yield return null; //1フレーム経過
        }
        // 終点座標へ補正
        isGround = true;
        transform.position = endPos;
        
    }
}

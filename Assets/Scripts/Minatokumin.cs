using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Minatokumin : ChikuminBase, IJampable, IDamageable
{
    //public ChikuminAiState aiState = ChikuminAiState.MOVE;
    NavMeshAgent agent;
    private GameObject targetObject;
    private GameObject goalObject;

    public CharacterStatus status;
    private bool isGround = false;

    public GameObject cursorObject; //カーソルを入れる。

    public float moveDis = 1.0f;

    private AudioSource audioSource;
    public AudioClip throwSE;

    Animator animator;

    private float _hp;

    //public List<GameObject> hitList = new List<GameObject>();

    void Start()
    {
        targetPlayer = GameObject.Find("Player");
        goalObject = GameObject.Find("Goal");
        agent = GetComponent<NavMeshAgent>();
        changeStatus();
        audioSource = GameObject.Find("SoundDirector").GetComponent<AudioSource>();
        animator = this.GetComponent<Animator>();
        _hp = status.hp;
    }

    // Update is called once per frame
    void Update()
    {
        //print(isHit);

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
            case ChikuminAiState.ALIGNMENT:
                Alignment();
                break;
            case ChikuminAiState.ONRUSH:
                OnRush();
                break;

        }

        animator.SetFloat("Speed", agent.velocity.sqrMagnitude);
        animator.SetBool("Have", carryObjectList.Count > 0);
        if (_hp <= 0)
        {
            Death();
        }


    }

    private void Wait()
    {
        prevState = ChikuminAiState.WAIT;
        
        agent.speed = 0;
        animator.SetBool("Attack", false);
        //print(isGround);
        if (!isGround)
        {

            return;
        }
        //今後の動作判定
        if (hitList.Count != 0)
        {
            targetObject = NearObject(hitList);
            //print(targetObject);
            if (targetObject.tag == "enemy")
            {


                hitList.Remove(targetObject);
                aiState = ChikuminAiState.ATTACK;
            }
            else if (targetObject.tag == "item")
            {
                hitList.Remove(targetObject);
                aiState = ChikuminAiState.CARRY;
            }
            else if (targetObject.tag == "atm")
            {
                //print("aaaaa");
                //targetObject.GetComponent<ATMController>().ReleaseMoney();
                //hitList.Remove(targetObject);
                //aiState = ChikuminAiState.WAIT;
            }
        }


    }
    private void Idle()
    {
        agent.speed = 0;
    }
    private void Move()
    {
        agent.stoppingDistance = 1.8f;
        animator.SetBool("Attack", false);

        changeStatus();
        agent.SetDestination(targetPlayer.transform.position);
        if (Vector3.Distance(targetPlayer.transform.position, this.transform.position) > moveDis)
        {

        }
        else
        {
           // agent.speed = 0;
        }
    }
    private void Attack()
    {
        //print("attack");
        //changeStatus();
        agent.stoppingDistance = 0f;
        if (!targetObject.gameObject.active)
        {
            isHit = false;
            aiState = ChikuminAiState.WAIT;
        }
        if (isHit)
        {
            agent.speed = 0;
            animator.SetBool("Attack", isHit);
            //targetObject.gameObject.GetComponent<IDamageable>().Damage(1);
        }
        else
        {
            changeStatus();
            agent.SetDestination(targetObject.transform.position);
        }


    }
    public void AttackDamage()
    {
        if (!targetObject.activeSelf)
        {
            return;
        }
        targetObject.gameObject.GetComponent<IDamageable>().Damage(status.level);
    }
    private void Carry()
    {
        

        agent.stoppingDistance = 0f;
        if (carryObjectList.Count == 0)
        {
            if (targetObject.GetComponent<Item>().maxCarryNum == targetObject.GetComponent<Item>().carryObjects.Count)
            {
                aiState = ChikuminAiState.WAIT;

            }

            if(targetObject.GetComponent<Item>().itemType == Item.Type.Jewelry)
            {
                isItem = true;
            }
            if (isItem == false)
            {
                changeStatus();
                agent.SetDestination(targetObject.transform.position);
                return;
            }
            if (targetObject.GetComponent<Item>().maxCarryNum > targetObject.GetComponent<Item>().carryObjects.Count)
            {
                isItem = false;
                carryObjectList.Add(targetObject.gameObject);
                carryObjectList[0].GetComponent<ICarriable>().Carry(this.gameObject);

                carryObjectList[0].GetComponent<Rigidbody>().isKinematic = true;
                carryObjectList[0].GetComponent<Rigidbody>().useGravity = false;

                carryObjectList[0].transform.parent = this.transform;
                carryObjectList[0].transform.localPosition = new Vector3(0, 1, 1);
                carryObjectList[0].transform.localRotation = Quaternion.Euler(0, 0, 0);
                agent.velocity = Vector3.zero;
            }
        }

        if(carryObjectList.Count <4+status.level && hitList.Count != 0)
        {

            print("carry");
            targetObject = NearObject(hitList);
            //二つ目以降のアイテムはひとりで運べるアイテムのみ
            if(targetObject.gameObject.tag == "item")
            {
                if (targetObject.GetComponent<Item>().maxCarryNum > targetObject.GetComponent<Item>().carryObjects.Count)
                {
                    if (targetObject.GetComponent<Item>().minCarryNum == 1)
                    {
                        if (targetObject.GetComponent<Item>().itemType == Item.Type.Jewelry)
                        {
                            isItem = true;
                        }
                        if (isItem == false)
                        {
                            changeStatus();
                            agent.SetDestination(targetObject.transform.position);
                            return;
                        }
                        isItem = false;
                        int i = carryObjectList.Count;

                        carryObjectList.Add(targetObject.gameObject);
                        carryObjectList[i].GetComponent<ICarriable>().Carry(this.gameObject);

                        carryObjectList[i].GetComponent<Rigidbody>().isKinematic = true;
                        carryObjectList[i].GetComponent<Rigidbody>().useGravity = false;

                        carryObjectList[i].transform.parent = this.transform;
                        carryObjectList[i].transform.localPosition = new Vector3(0, 1+i * 0.1f, 1);
                        carryObjectList[i].transform.localRotation = Quaternion.Euler(0, 0, 0);
                        agent.velocity = Vector3.zero;
                    }
                }

            }
            hitList.Remove(targetObject);

        }

        if(hitList.Count ==0 &&  carryObjectList.Count == 0)
        {
           // aiState = prevState;
        }

        if (carryObjectList.Count == 0)
        {
            return;
        }


        //carryObject.transform.position = this.transform.position + (Vector3.forward*-0.5f);

        if (carryObjectList[0].GetComponent<Item>().minCarryNum <= carryObjectList[0].GetComponent<Item>().carryObjects.Count)
        {
            agent.speed = _carrySpeed;
            agent.SetDestination(goalObject.transform.position);
        }
        else if (carryObjectList[0].GetComponent<Item>().minCarryNum > carryObjectList[0].GetComponent<Item>().carryObjects.Count)
        {
            agent.speed = 0;
        }
        //Move();
    }
    private void Alignment()
    {
        if (waitArea != null)
        {
           // agent.SetDestination(waitPos);
        }
        if (Vector3.Distance(waitPos, this.transform.position) >1)
        {
            agent.SetDestination(waitPos);
        }
        else
        {
            aiState = ChikuminAiState.WAIT;
        }

    }

    private void OnRush()
    {


        cursorObject = GameObject.Find("Reticle(Clone)");
        agent.SetDestination(cursorObject.transform.position);

        //この下に状況判断を書く　waitと同じなので状況判断のメソッドにまとめる。
        if (hitList.Count != 0)
        {
            targetObject = NearObject(hitList);
            //print(targetObject);
            if (targetObject.tag == "enemy")
            {


                hitList.Remove(targetObject);
                aiState = ChikuminAiState.ATTACK;
            }
            else if (targetObject.tag == "item")
            {
                hitList.Remove(targetObject);
                aiState = ChikuminAiState.CARRY;
            }
            else if (targetObject.tag == "atm")
            {
                //print("aaaaa");
                //targetObject.GetComponent<ATMController>().ReleaseMoney();
                //hitList.Remove(targetObject);
                //aiState = ChikuminAiState.WAIT;
            }
        }

    }


    private GameObject NearObject(List<GameObject> gameObjects)
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

    private void changeStatus()
    {
        agent.speed = status.moveSpeed;
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
    public void Death()
    {
        gameObject.SetActive(false);
    }
    public void Damage(int value)
    {
        _hp -= value;
    }
}

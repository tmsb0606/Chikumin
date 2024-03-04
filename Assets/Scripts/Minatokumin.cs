using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Minatokumin : ChikuminBase, IJampable
{
    //public ChikuminAiState aiState = ChikuminAiState.MOVE;
    NavMeshAgent agent;
    private GameObject targetObject;
    private GameObject goalObject;

    public CharacterStatus status;
    private bool isHit = false;
    private bool isItem = false;
    private bool isGround = false;

    public GameObject cursorObject; //カーソルを入れる。

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

    }

    private void Wait()
    {
        prevState = ChikuminAiState.WAIT;
        
        agent.speed = 0;
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
        if (Vector3.Distance(targetPlayer.transform.position, this.transform.position) > moveDis)
        {
            changeStatus();
            agent.SetDestination(targetPlayer.transform.position);
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
        if (isHit)
        {
            agent.speed = 0;
            targetObject.gameObject.GetComponent<IDamageable>().Damage(1);
        }
        else
        {
            changeStatus();
            agent.SetDestination(targetObject.transform.position);
        }


    }
    private void Carry()
    {
       
/*        if (carryObjectList.Count < 5 && hitList.Count != 0)
        {
            carryObjectList.Add(targetObject.gameObject);
            int i = 0;

            while (true)
            {
               
                if (carryObjectList[i].GetComponent<Item>().maxCarryNum > carryObjectList[i].GetComponent<Item>().carryObjects.Count)
                {
                    if (carryObjectList[i].gameObject.tag == "item")
                    {
                        carryObjectList[i].GetComponent<ICarriable>().Carry(this.gameObject);

                        carryObjectList[i].GetComponent<Rigidbody>().isKinematic = true;
                        carryObjectList[i].GetComponent<Rigidbody>().useGravity = false;

                        carryObjectList[i].transform.parent = this.transform;
                        carryObjectList[i].transform.localPosition = new Vector3(0, i * 0.1f, 1);
                        carryObjectList[i].transform.localRotation = Quaternion.Euler(0, 0, 0);
                        i++;
                        carryObjectList.Remove(targetObject);
                        if (hitList.Count != 0)
                        {
                            carryObjectList.Add(NearObject(hitList));
                            hitList.Remove(NearObject(hitList));
                            continue;

                        }
                    }
                    else
                    {
                        carryObjectList.Remove(targetObject);
                        if (hitList.Count != 0)
                        {
                            carryObjectList.Add(NearObject(hitList));
                            hitList.Remove(NearObject(hitList));
                            continue;

                        }
                    }

                }
                if (i >= 0)
                {
                    break;
                }
            }
  
        }*/

        if(carryObjectList.Count == 0)
        {
            carryObjectList.Add(targetObject.gameObject);
            carryObjectList[0].GetComponent<ICarriable>().Carry(this.gameObject);

            carryObjectList[0].GetComponent<Rigidbody>().isKinematic = true;
            carryObjectList[0].GetComponent<Rigidbody>().useGravity = false;

            carryObjectList[0].transform.parent = this.transform;
            carryObjectList[0].transform.localPosition = new Vector3(0, 0, 1);
            carryObjectList[0].transform.localRotation = Quaternion.Euler(0, 0, 0);
        }

        if(carryObjectList.Count <4+status.level && hitList.Count != 0)
        {

            print("carry");
            targetObject = NearObject(hitList);
            //二つ目以降のアイテムは札束のみ。札束以外は複数個持てない。
            if(targetObject.gameObject.tag == "item")
            {
                if (targetObject.GetComponent<Item>().maxCarryNum > targetObject.GetComponent<Item>().carryObjects.Count)
                {
                    int i = carryObjectList.Count;

                    carryObjectList.Add(targetObject.gameObject);
                    carryObjectList[i].GetComponent<ICarriable>().Carry(this.gameObject);

                    carryObjectList[i].GetComponent<Rigidbody>().isKinematic = true;
                    carryObjectList[i].GetComponent<Rigidbody>().useGravity = false;

                    carryObjectList[i].transform.parent = this.transform;
                    carryObjectList[i].transform.localPosition = new Vector3(0, i * 0.1f, 1);
                    carryObjectList[i].transform.localRotation = Quaternion.Euler(0, 0, 0);
                }

            }
            hitList.Remove(targetObject);

        }

        if(hitList.Count ==0 &&  carryObjectList.Count == 0)
        {
           // aiState = prevState;
        }

        
        //carryObject.transform.position = this.transform.position + (Vector3.forward*-0.5f);

        changeStatus();
        agent.SetDestination(goalObject.transform.position);
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


        cursorObject = GameObject.Find("piku(Clone)");
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
}

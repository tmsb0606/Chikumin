using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Adachikumin : ChikuminBase,IJampable
{
    //public ChikuminAiState aiState = ChikuminAiState.MOVE;
    NavMeshAgent agent;

    public GameObject targetObject;
    private GameObject goalObject;

    public GameObject cursorObject; //カーソルを入れる。
    
    public CharacterStatus status;


    //private bool isHit = false;
    
    private bool isGround = true;
    private bool isStop = false;

    public float moveDis = 1.0f;

    private AudioSource audioSource;
    public AudioClip throwSE;

    Animator animator;

    Rigidbody rb;

    //public List<GameObject> hitList = new List<GameObject>();

    void Start()
    {
        targetPlayer = GameObject.Find("Player");
        goalObject = GameObject.Find("Goal");
        agent = GetComponent<NavMeshAgent>();
        changeStatus();
        audioSource = GameObject.Find("SoundDirector").GetComponent<AudioSource>();
        animator = this.GetComponent<Animator>();

/*        agent.updatePosition = false;
        agent.updateRotation = false;*/
        rb = GetComponent<Rigidbody>();
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

/*        changeStatus();
        agent.SetDestination(targetPlayer.transform.position);*/




    }

    private void Wait()
    {
/*        agent.updatePosition = true;
        agent.updateRotation = true;*/
        prevState = ChikuminAiState.WAIT;
        agent.speed = 0;
        //print(isGround);
        if (!isGround)
        {
            print("!ground");
            return;
        }
        //今後の動作判定
        if(hitList.Count != 0)
        {
            print("wait : list > 0");
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
        changeStatus();
        agent.stoppingDistance = 1.8f;
        print(gameObject.name+"ステータス更新");
        
        agent.SetDestination(targetPlayer.transform.position);
        print(gameObject.name + "移動");
        /*        if (Vector3.Distance(targetPlayer.transform.position, this.transform.position) > moveDis)
                {
                    changeStatus();
                    agent.SetDestination(targetPlayer.transform.position);
                    //OnManualMove();



                }
                else
                {
                    agent.velocity = Vector3.zero;

                }*/
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
            /*            agent.updatePosition = true;
                        agent.updateRotation = true;*/

        }
        else
        {
            changeStatus();
            agent.SetDestination(targetObject.transform.position);
            //OnManualMove();
            print("AttackMove");
        }
        
        
    }
    public void AttackDamage()
    {
        if (!targetObject.activeSelf)
        {
            return;
        }
        targetObject.gameObject.GetComponent<IDamageable>().Damage(10 * status.level);
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
            if (targetObject.GetComponent<Item>().itemType == Item.Type.Jewelry)
            {
                isItem = true;
            }
            if (isItem == false)
            {
                changeStatus();
                agent.SetDestination(targetObject.transform.position);
                //OnManualMove();
                return;
            }
            isItem = false;
            carryObjectList.Add(targetObject.gameObject);
            if (carryObjectList[0].GetComponent<Item>().maxCarryNum > carryObjectList[0].GetComponent<Item>().carryObjects.Count)
            {

                carryObjectList[0].GetComponent<ICarriable>().Carry(this.gameObject);

                carryObjectList[0].GetComponent<Rigidbody>().isKinematic = true;
                carryObjectList[0].GetComponent<Rigidbody>().useGravity = false;

                if (carryObjectList[0].GetComponent<Item>().carryObjects.Count == 1)
                {
                    agent.velocity = Vector3.zero;
                    //agent.Stop();
                    carryObjectList[0].transform.parent = this.transform;
                    carryObjectList[0].transform.localPosition = new Vector3(0, 1, 1);
                    carryObjectList[0].transform.localRotation = Quaternion.Euler(0, 0, 0);
                }

            }
            else
            {
                //carryObjectList[0] = null;
                aiState = prevState;
                carryObjectList.Clear();
            }

            //時間があったらアイテムの方向に移動して拾うモーションが欲しい。
            //changeStatus();
            //agent.SetDestination(targetObject.transform.position);

        }
        //carryObject.transform.position = this.transform.position + (Vector3.forward*-0.5f);
        if(carryObjectList.Count == 0)
        {
            return;
        }
        if(carryObjectList[0].GetComponent<Item>().minCarryNum <= carryObjectList[0].GetComponent<Item>().carryObjects.Count)
        {
            changeStatus();
            agent.SetDestination(goalObject.transform.position);
            //OnManualMove();
        }
        else if(carryObjectList[0].GetComponent<Item>().minCarryNum > carryObjectList[0].GetComponent<Item>().carryObjects.Count)
        {
            agent.speed = 0;
        }

        //Move();
    }

    private void Alignment()
    {
        if (waitArea != null)
        {
            //agent.SetDestination(waitArea.transform.position);
        }
        if (Vector3.Distance(waitPos, this.transform.position) > 1)
        {
            agent.SetDestination(waitPos);
            //OnManualMove();
        }
        else
        {
            aiState = ChikuminAiState.WAIT;
/*            agent.updatePosition = true;
            agent.updateRotation = true;*/
        }
    }

    private void OnRush()
    {
        prevState = ChikuminAiState.ONRUSH;
        cursorObject =  GameObject.Find("piku(Clone)");
        agent.SetDestination(cursorObject.transform.position);
        //OnManualMove();

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

    private void OnManualMove()
    {
        if (isGround)
        {
            agent.updatePosition = false;
            agent.updateRotation = false;
        }

        // 次の位置への方向を求める
        var dir = agent.nextPosition - transform.position;

        // 方向と現在の前方との角度を計算（スムーズに回転するように係数を掛ける）
        float smooth = Mathf.Min(1.0f, Time.deltaTime / 0.15f);
        var angle = Mathf.Acos(Vector3.Dot(transform.forward, dir.normalized)) * Mathf.Rad2Deg * smooth;

        // 回転軸を計算
        var axis = Vector3.Cross(transform.forward, dir);

        // 回転の更新
        var rot = Quaternion.AngleAxis(angle, axis);
        transform.forward = rot * transform.forward;

        // 位置の更新
        transform.position = agent.nextPosition;




        /*        // 次に目指すべき位置を取得
                var nextPoint = agent.steeringTarget;
                Vector3 targetDir = nextPoint - transform.position;

                // その方向に向けて旋回する(360度/秒)
                Quaternion targetRotation = Quaternion.LookRotation(targetDir);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 360f * Time.deltaTime);

                // 自分の向きと次の位置の角度差が30度以上の場合、その場で旋回
                float angle = Vector3.Angle(targetDir, transform.forward);
                if (angle < 30f)
                {
                    transform.position += transform.forward * 5.0f * Time.deltaTime;
                    // もしもの場合の補正
                    //if (Vector3.Distance(nextPoint, transform.position) < 0.5f)　transform.position = nextPoint;
                }

                // targetに向かって移動します。
                agent.SetDestination(targetPlayer.transform.position);
                agent.nextPosition = transform.position;*/


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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "ground" && other.gameObject.tag != "Player"&& other.gameObject.tag !="search" && other.gameObject.tag != "tiku" && other.gameObject.tag != "Untagged")
        {
            isStop = true;
        }


    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "enemy")
        {
            isHit = false;
        }

        if (other.gameObject.tag != "ground" && other.gameObject.tag != "Player" && other.gameObject.tag != "search" && other.gameObject.tag != "tiku" && other.gameObject.tag != "Untagged")
        {
            isStop = false;
        }
    }


    public IEnumerator Jump(Vector3 endPos, float flightTime, float speedRate, float gravity)
    {
        changeStatus();
        audioSource.PlayOneShot(throwSE);
        agent.enabled = false;
        isGround = false;
        
        var startPos = transform.position; // 初期位置
        var diffY = (endPos - startPos).y; // 始点と終点のy成分の差分
        var vn = (diffY - gravity * 0.5f * flightTime * flightTime) / flightTime; // 鉛直方向の初速度vn

        // 放物運動
        for (var t = 0f; t < flightTime; t += (Time.deltaTime * speedRate))
        {
            print(t);

            var p = Vector3.Lerp(startPos, endPos, t / flightTime);   //水平方向の座標を求める (x,z座標)
            if (isStop)
            {
                print("ストップだよ");
                endPos = this.transform.position;
                p.x = this.transform.position.x;
                p.z = this.transform.position.z;


            }

            p.y = startPos.y + vn * t + 0.5f * gravity * t * t; // 鉛直方向の座標 y
            
            transform.position = p;

            yield return null; //1フレーム経過
        }
        // 終点座標へ補正
        isGround = true;
        transform.position = endPos;
        agent.enabled = true;
/*        agent.updatePosition = true;
        agent.updateRotation = true;*/

    }
}

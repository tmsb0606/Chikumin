using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Adachikumin : ChikuminBase,IJampable,IDamageable
{
    //public ChikuminAiState aiState = ChikuminAiState.MOVE;
    private GameObject goalObject;

    public GameObject cursorObject; //�J�[�\��������B
    
    public CharacterStatus status;


    //private bool isHit = false;
    
    private bool isGround = true;
    private bool isStop = false;

    public float moveDis = 1.0f;

    private AudioSource audioSource;
    public AudioClip throwSE;

    Animator animator;

    Rigidbody rb;
    private int _hp;

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
        animator.SetBool("Attack", false);
        if (!isGround)
        {
            print("!ground");
            return;
        }

        
        ActionJudgment();


    }
    private void Idle()
    {
        agent.speed = 0;
    }
    private void Move()
    {
        changeStatus();
        animator.SetBool("Attack", false);

        agent.stoppingDistance = 1.8f;
        
        agent.SetDestination(targetPlayer.transform.position);
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

            //���Ԃ���������A�C�e���̕����Ɉړ����ďE�����[�V�������~�����B
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
            agent.speed = _carrySpeed;
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

        if (Vector3.Distance(waitPos, this.transform.position) > 1)
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
        prevState = ChikuminAiState.ONRUSH;
        cursorObject =  GameObject.Find("Reticle(Clone)");
        agent.SetDestination(cursorObject.transform.position);
        ActionJudgment();

    }

    /// <summary>
    /// ����̍s���𔻒f����B(�ˌ��A�ҋ@��)
    /// </summary>
    private void ActionJudgment()
    {
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
        }
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
        
        var startPos = transform.position; // �����ʒu
        var diffY = (endPos - startPos).y; // �n�_�ƏI�_��y�����̍���
        var vn = (diffY - gravity * 0.5f * flightTime * flightTime) / flightTime; // ���������̏����xvn

        // �����^��
        for (var t = 0f; t < flightTime; t += (Time.deltaTime * speedRate))
        {
            print(t);

            var p = Vector3.Lerp(startPos, endPos, t / flightTime);   //���������̍��W�����߂� (x,z���W)
            if (isStop)
            {
                print("�X�g�b�v����");
                endPos = this.transform.position;
                p.x = this.transform.position.x;
                p.z = this.transform.position.z;


            }

            p.y = startPos.y + vn * t + 0.5f * gravity * t * t; // ���������̍��W y
            
            transform.position = p;

            yield return null; //1�t���[���o��
        }
        // �I�_���W�֕␳
        isGround = true;
        transform.position = endPos;
        agent.enabled = true;

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

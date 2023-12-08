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
    private bool isPlayer = false;
    private bool isEnemy = false;
    private bool isItem = false;
    private List<GameObject> hitList = new List<GameObject>();

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
            case ChikuminAiState.IDLE:
                Idle();
                break;
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
        agent.speed = 0;
        //����̓��씻��
        if(hitList.Count != 0)
        {
            print(hitList[0]);
        }
        

    }
    private void Idle()
    {
        agent.speed = 0;
    }
    private void Move()
    {
        changeStatus();
        agent.SetDestination(targetPlayer.transform.position);
    }
    private void  changeStatus()
    {
        agent.speed = status.moveSpeed;
    }
    public void OnTriggerEnter(Collider other)
    {
        this.GetComponent<Rigidbody>().isKinematic = true;
       
       if (other.gameObject.tag != "ground"&& other.gameObject.tag != "tiku")
        {
            hitList.Add(other.gameObject);
        }
    }
    public void OnTriggerExit(Collider other)
    {
        //this.GetComponent<Rigidbody>().isKinematic = true;
        if (other.gameObject.tag != "ground" && other.gameObject.tag != "tiku")
        {
            hitList.Remove(other.gameObject);
        }

    }
    public IEnumerator Jump(Vector3 endPos, float flightTime, float speedRate, float gravity)
    {
        //if (isFlag)
        //{
            //yield break;
        //}
        var startPos = transform.position; // �����ʒu
        var diffY = (endPos - startPos).y; // �n�_�ƏI�_��y�����̍���
        var vn = (diffY - gravity * 0.5f * flightTime * flightTime) / flightTime; // ���������̏����xvn

        // �����^��
        for (var t = 0f; t < flightTime; t += (Time.deltaTime * speedRate))
        {
            var p = Vector3.Lerp(startPos, endPos, t / flightTime);   //���������̍��W�����߂� (x,z���W)
            p.y = startPos.y + vn * t + 0.5f * gravity * t * t; // ���������̍��W y
            transform.position = p;
            yield return null; //1�t���[���o��
        }
        // �I�_���W�֕␳
        transform.position = endPos;
    }
}

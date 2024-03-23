using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChikuminTest : ChikuminBase,IJampable
{
    private static new readonly StateWaiting stateWaiting = new StateWaiting();
    private static new readonly StateMoving stateMoving = new StateMoving();
    private static new readonly StateCarrying stateCarrying = new StateAcachikuminCarrying();


    private PlayerController _playerController;

/*    public override  StateWaiting stateWaiting { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public override StateMoving stateMoving { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public override StateCarrying stateCarrying { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public override ChikuminStateBase currentState { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }*/






    // Start is called before the first frame update

    private void OnStart()
    {
        
    }
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        //targetPlayer = GameObject.Find("Player");
        _playerController = targetPlayer.GetComponent<PlayerController>();
        currentState.OnEnter(this, null);
    }

    // Update is called once per frame
    void Update()
    {
        
        currentState.OnUpdata(this);
    }

    private void ChangeState(ChikuminStateBase nextState)
    {
        currentState.OnExit(this, nextState);
        nextState.OnEnter(this, currentState);
        currentState = nextState;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "circle")
        {
            var player = _playerController;
            if(player.mouseState == PlayerController.MouseState.callTiku)
            {
                ChangeState(stateMoving);
                if (!player.callTikuminList.Contains(this.gameObject))
                {
                    player.callTikuminList.Add(this.gameObject);
                }
                
            }

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "item")
        {
            if (!carryObjectList.Contains(other.gameObject))
            {
                carryObjectList.Add(other.gameObject);
                //ChangeState(stateCarrying);
            }
        }
    }

    public IEnumerator Jump(Vector3 endPos, float flightTime, float speedRate, float gravity)
    {
        ChangeState(stateWaiting);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public enum MouseState
    {
        nothing,
        waitTiku,
        callTiku,
        throwTiku,

    }
    private Rigidbody rb;

    public float speed = 10f;
    private float movementX;
    private float movementY;



    private Camera mainCamera;
    private Vector3 currentPosition = Vector3.zero;
    public GameObject circle;
    private GameObject pointCircle;
    public MouseState mouseState = MouseState.nothing;

    public List<GameObject> callTikuminList = new List<GameObject>();

    [SerializeField, Range(0F, 90F), Tooltip("�ˏo����p�x")]
    private float ThrowingAngle;

    [SerializeField]
    private LayerMask layerMask;



    [SerializeField] Transform endPos;  //�I�_���W
    [SerializeField] float flightTime = 2;  //�؋󎞊�
    [SerializeField] float speedRate = 1;   //�؋󎞊Ԃ���Ƃ����ړ����x�{��
    private const float gravity = -9.8f;    //�d��

    void Start()
    {
        Cursor.visible = false;
        mouseState = MouseState.nothing;
        mainCamera = Camera.main;
        rb = gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        //print(mouseState);
        drawCursor();
        if (Input.GetMouseButton(0))
        {
            CallChikumin();
        }
        // ���͒l������3���x�N�g�����쐬
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        // rigidbody��AddForce���g�p���ăv���C���[�𓮂����B
        rb.AddForce(movement * speed);
    }

    void OnDrawGizmos()
    {
        if (currentPosition != Vector3.zero)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(currentPosition, 0.5f);
        }
    }
    private void drawCursor()
    {
        //int layerMask = 1 << 6;
        //int layerMask = LayerMask.GetMask(new string[] { LayerMask.LayerToName(6) });
        var ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        var raycastHitList = Physics.RaycastAll(ray,layerMask).ToList();
       // print(raycastHitList);
        if (raycastHitList.Any())
        {
            if (pointCircle == null)
            {
                pointCircle = Instantiate(circle);
            }
            var distance = Vector3.Distance(mainCamera.transform.position, raycastHitList.First().point);
            var mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);

            currentPosition = mainCamera.ScreenToWorldPoint(mousePosition);
           // print(currentPosition);
            currentPosition.y = 0;
            pointCircle.transform.position = currentPosition;
            pointCircle.GetComponent<CallCircle>().player = this;
        }
        else
        {
            if (pointCircle != null)
            {
                pointCircle.SetActive(false);
                pointCircle = null;

            }

        }
    }
    private void CallChikumin()
    {
       // pointCircle.GetComponent<>
    }


    private void OnMove(InputValue movementValue)
    {
        // Move�A�N�V�����̓��͒l���擾
        Vector2 movementVector = movementValue.Get<Vector2>();

        // x,y�������̓��͒l��ϐ��ɑ��
        movementX = movementVector.x;
        movementY = movementVector.y;
    }
    private void OnCall()
    {
        print("call");
        mouseState = MouseState.callTiku;
    }
    private void OnStay()
    {
        mouseState = MouseState.waitTiku;
    }
    private void OnCancel()
    {
        print("cancel");
        mouseState = MouseState.nothing;
    }
    private void OnThrow()
    {

        Vector3 endPos = new Vector3(pointCircle.transform.position.x, pointCircle.transform.position.y + 1, pointCircle.transform.position.z);
        StartCoroutine(callTikuminList[0].gameObject.GetComponent<Adachikumin>().Jump(endPos, flightTime, speedRate, gravity));
        callTikuminList[0].gameObject.GetComponent<Adachikumin>().aiState = ChikuminBase.ChikuminAiState.WAIT;
        callTikuminList.Remove(callTikuminList[0]);


    }



}

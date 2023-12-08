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

    [SerializeField, Range(0F, 90F), Tooltip("射出する角度")]
    private float ThrowingAngle;

    [SerializeField]
    private LayerMask layerMask;



    [SerializeField] Transform endPos;  //終点座標
    [SerializeField] float flightTime = 2;  //滞空時間
    [SerializeField] float speedRate = 1;   //滞空時間を基準とした移動速度倍率
    private const float gravity = -9.8f;    //重力

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
        // 入力値を元に3軸ベクトルを作成
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        // rigidbodyのAddForceを使用してプレイヤーを動かす。
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
        // Moveアクションの入力値を取得
        Vector2 movementVector = movementValue.Get<Vector2>();

        // x,y軸方向の入力値を変数に代入
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

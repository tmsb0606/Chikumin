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
    public List<GameObject> hitTikuminList = new List<GameObject>();

    [SerializeField, Range(0F, 90F), Tooltip("射出する角度")]
    private float ThrowingAngle;

    [SerializeField]
    private LayerMask layerMask;



    [SerializeField] Transform endPos;  //終点座標
    [SerializeField] float flightTime = 2;  //滞空時間
    [SerializeField] float speedRate = 1;   //滞空時間を基準とした移動速度倍率
    private const float gravity = -9.8f;    //重力
    Vector3 latestPos;

    private AudioSource audioSource;
    public AudioClip ComeOnSE;
    public AudioClip WaitSE;

    //[SerializeField] private GameObject _camera;

    [SerializeField] private GameObject _cameraObject;
     private CameraController _camera;

    void Start()
    {
        Cursor.visible = false;
        mouseState = MouseState.nothing;
        mainCamera = Camera.main;
        rb = gameObject.GetComponent<Rigidbody>();
        audioSource = GameObject.Find("SoundDirector").GetComponent<AudioSource>();
        _camera = _cameraObject.GetComponent<CameraController>();
    }

    void FixedUpdate()
    {
        //print(mouseState);
        //print(hitTikuminList.Count);
        drawCursor();
        if (Input.GetMouseButton(0))
        {
            CallChikumin();
        }
        // 入力値を元に3軸ベクトルを作成
        //float Mathf.Lerp(_camera.prevYRota, _cameraObject.transform.localEulerAngles.y,Time.deltaTime);
        float angle = (-_cameraObject.transform.localEulerAngles.y)* Mathf.Deg2Rad;
        //print(Mathf.Sin( angle));
        //print(angle);
        float x = -Mathf.Sin(angle)*movementY +Mathf.Cos(angle)*movementX;
        float y = Mathf.Sin(angle) * movementX + Mathf.Cos(angle) * movementY;
        Vector3 movement = new Vector3(x, 0.0f, y);
        //print(movementX + " : " + movementY);
         //movement =movement.normalized;

        // rigidbodyのAddForceを使用してプレイヤーを動かす。

        //Vector3 vec = movement * _camera.transform.localEulerAngles.y;
        rb.AddForce(movement * speed);

        Vector3 differenceDis = new Vector3(transform.position.x, 0, transform.position.z) - new Vector3(latestPos.x, 0, latestPos.z);
        latestPos = transform.position;
        if (Mathf.Abs(differenceDis.x) > 0.001f || Mathf.Abs(differenceDis.z) > 0.001f)
        {
            if (movement == new Vector3(0, 0, 0)) return;
            Quaternion rot = Quaternion.LookRotation(differenceDis);
            rot = Quaternion.Slerp(rb.transform.rotation, rot, 0.2f);
            this.transform.rotation = rot;
        }
        

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
        audioSource.PlayOneShot(ComeOnSE);
        mouseState = MouseState.callTiku;
    }
    private void OnStay()
    {
        mouseState = MouseState.waitTiku;
        audioSource.PlayOneShot(WaitSE);
        foreach(GameObject c in callTikuminList)
        {
            c.GetComponent<ChikuminBase>().aiState = ChikuminBase.ChikuminAiState.ALIGNMENT;
            //callTikuminList.Remove(c);
        }
        callTikuminList.Clear();
    }
    private void OnCancel()
    {
        //print("cancel");
        mouseState = MouseState.nothing;
    }
    private void OnThrow()
    {
        GameObject obj = NearObject(callTikuminList);
        if(obj == null)
        {
            return;
        }
        Vector3 endPos = new Vector3(pointCircle.transform.position.x, pointCircle.transform.position.y + 1, pointCircle.transform.position.z);
        StartCoroutine(obj.gameObject.GetComponent<IJampable>().Jump(endPos, flightTime, speedRate, gravity));
        obj.gameObject.GetComponent<ChikuminBase>().aiState = ChikuminBase.ChikuminAiState.WAIT;
        callTikuminList.Remove(obj);


    }
    private void OnRotaionCamera()
    {
        //print("rotaion");
        //float x = _camera.transform.position.x * Mathf.Cos(this.transform.rotation.y) - _camera.transform.position.z*Mathf.Sin(this.transform.rotation.y);
        //float z = _camera.transform.position.z * Mathf.Cos(this.transform.rotation.y) - _camera.transform.position.x * Mathf.Sin(this.transform.rotation.y);
        //_camera.GetComponent<CinemachineCameraOffset>().m_Offset = new Vector3(x, _camera.transform.position.y, z);
        //_camera.transform.position = new Vector3(x, _camera.transform.position.y, z);
        _camera.Rotaion();
    }

    private GameObject NearObject(List<GameObject> gameObjects)
    {
        foreach (GameObject a in hitTikuminList)
        {
            print(a);
        }
        if (gameObjects.Count == 0)
        {
            return null;
        }
        if (gameObjects.Count == 1)
        {
            if (hitTikuminList.Contains(gameObjects[0]))
            {
                return gameObjects[0];
            }
            else
            {
                return null;
            }
        }

        GameObject nearObj = gameObjects[0];
     
        //gameObjects.Remove(gameObjects[0]);
        float dis = Vector3.Distance(nearObj.transform.position, this.transform.position);
        foreach (GameObject obj in gameObjects)
        {
            print(hitTikuminList.Contains(obj));

            //print("aiueo");

            float dis2 = Vector3.Distance(obj.transform.position, this.transform.position);

            if (dis >= dis2)
            {
                dis = dis2;
                nearObj = obj;
            }


        }
        if (!hitTikuminList.Contains(nearObj))
        {
            return null;
        }
        return nearObj;
    }



}

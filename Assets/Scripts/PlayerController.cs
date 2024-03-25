using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.InputSystem;
using System;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public enum MouseState
    {
        nothing,
        waitTiku,
        callTiku,
        throwTiku,
        rushTiku,

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
    public AudioClip GoSE;

    //[SerializeField] private GameObject _camera;

    [SerializeField] private GameObject _cameraObject;
     private CameraController _camera;



    [SerializeField] private GameStateController _gameStateController;

    public GameObject particle;

    private ParticleScript _particleScript; 


    void Start()
    {
        
        mouseState = MouseState.nothing;
        mainCamera = Camera.main;
        rb = gameObject.GetComponent<Rigidbody>();
        audioSource = GameObject.Find("SoundDirector").GetComponent<AudioSource>();
        _camera = _cameraObject.GetComponent<CameraController>();
        _particleScript = particle.GetComponent<ParticleScript>();
    }

    void FixedUpdate()
    {

        if(_gameStateController.currentState != _gameStateController.playState)
        {
            return;
        }
        drawCursor();
        if (Input.GetMouseButton(0))
        {
            CallChikumin();
        }
        // 入力値を元に3軸ベクトルを作成
        float angle = (-_cameraObject.transform.localEulerAngles.y)* Mathf.Deg2Rad;


        //ベクトルを回転
        float x = -Mathf.Sin(angle)*movementY +Mathf.Cos(angle)*movementX;
        float y = Mathf.Sin(angle) * movementX + Mathf.Cos(angle) * movementY;
        Vector3 movement = new Vector3(x, 0.0f, y) *speed;


        // rigidbodyのAddForceを使用してプレイヤーを動かす。

        rb.AddForce(movement-rb.velocity);

        Vector3 differenceDis = new Vector3(transform.position.x, 0, transform.position.z) - new Vector3(latestPos.x, 0, latestPos.z);
        latestPos = transform.position;
        if (Mathf.Abs(differenceDis.x) > 0.001f || Mathf.Abs(differenceDis.z) > 0.001f)
        {
            if (movement == new Vector3(0, 0, 0)) return;
            Quaternion rot = Quaternion.LookRotation(differenceDis);
            rot = Quaternion.Slerp(rb.transform.rotation, rot, 0.2f);
            this.transform.rotation = rot;
        }

        if(rb.velocity.sqrMagnitude <= 0.5f)
        {
            Rotation();
        }




    }

    private void Rotation()
    {
        //transform.rotation = Quaternion.Euler(0, (getAngle(pointCircle.transform.position, transform.position)*-1-90), 0);
        Quaternion rot =  Quaternion.Slerp(rb.transform.rotation, Quaternion.Euler(0, (getAngle(pointCircle.transform.position, transform.position) * -1 - 90), 0),0.2f);
        this.transform.rotation = rot;
    }
    private float getAngle(Vector3 pos1, Vector3 pos2)
    {

        float angle = Mathf.Atan2(pos2.z - pos1.z, pos2.x - pos1.x) * Mathf.Rad2Deg;
        // print("角度" + angle);
        return angle;
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

        

        foreach (var hit in raycastHitList)
        {
            //print("レイキャスト:" + raycastHitList[0].collider);
            if (hit.collider.tag == "ground")
            {
                if (pointCircle == null)
                {
                    pointCircle = Instantiate(circle);
                }
                var distance = Vector3.Distance(mainCamera.transform.position, hit.point);
                var mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);

                currentPosition = mainCamera.ScreenToWorldPoint(mousePosition);
                // print(currentPosition);
                currentPosition.y = 0;
                pointCircle.transform.position = currentPosition;
                pointCircle.GetComponent<CallCircle>().player = this;
            }
        }
        particle.transform.position = pointCircle.transform.position + new Vector3(0,0.2f,0);
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
        if (_gameStateController.currentState != _gameStateController.playState)
        {
            return;
        }
        _particleScript.isScaling = true;
        particle.SetActive(true);
        print("call");
        audioSource.PlayOneShot(ComeOnSE);
        mouseState = MouseState.callTiku;

        
        

    }

    private void OnReleaseMouse()
    {
        if (_gameStateController.currentState != _gameStateController.playState)
        {
            return;
        }
        _particleScript.isScaling = false;
        print("release");
    }
    private void OnStay()
    {

        

        //ここから下に新しい整列を書く
        LayerMask layer= ~(1 << 12 | 1 << 13 | 1 <<14 | 1 << 6 | 1<<15 | 1<<16);
        int cnt = 0; //配置できた円の数
        int limit = 40;
        int r = 2;
        int Yoffset = 5;


        //print(Physics.CheckSphere(pos, 4, layer));
        List<Vector3> callPosList = new List<Vector3>(9);
        
        for(int i = 0; i < limit; i+=1)
        {
            bool flag = false;
            for(int j = 0; j < limit; j+=1)
            {
                //原点を中心に順番に座標を取得する
                int x = (int)((Step(j)-0.5f)*2)*j;
                int z = (int)((Step(i) - 0.5f) * 2)*(i+Yoffset);
                Vector3 vec = new Vector3(x, 0, z) ;
            


                float angle = (-this.transform.localEulerAngles.y) * Mathf.Deg2Rad;
                //print(Mathf.Sin( angle));
                //print(angle);

                //原点を中心に取得した座標をプレイヤーの向いている向きに回転
                float rx = -Mathf.Sin(angle) * vec.z + Mathf.Cos(angle) * vec.x;
                float rz = Mathf.Sin(angle) * vec.x + Mathf.Cos(angle) * vec.z;
                print("x:"+rx+"y:"+rz);
                print("正面" + transform.forward);

                //回転した座標とプレイヤーの座標を加算することでプレイヤー付近の座標を順番に取得する
                Vector3 pos = this.transform.position + new Vector3(rx, 0, rz);


                if (!Physics.CheckSphere(pos, 4, layer)&& Physics.CheckSphere(pos, 4, 1 << 12))
                {

                    cnt++;
                    flag = true;
                    //j += r;
                    callPosList.Add(pos);

                    if (cnt > 2)
                    {
                        break;
                    }
                    
                }
                

            }
            if (flag)
            {
                //i += r;
            }
            
            if (cnt > 2)
            {
                break;
            }
        }

        foreach (GameObject c in callTikuminList)
        {
            ChikuminBase chikumin = c.GetComponent<ChikuminBase>();
            chikumin.aiState = ChikuminBase.ChikuminAiState.ALIGNMENT;

            var classType = chikumin.GetType();
            if (classType == typeof(Adachikumin))
            {
                chikumin.waitPos = callPosList[0];
            }
            else if(classType == typeof(Chiyodakumin))
            {
                chikumin.waitPos = callPosList[1];
            }
            else
            {
                chikumin.waitPos = callPosList[2];
            }



        }


        mouseState = MouseState.waitTiku;
        audioSource.PlayOneShot(WaitSE);
        callTikuminList.Clear();
    }
    private void OnCancel()
    {
        //print("cancel");
        mouseState = MouseState.nothing;
    }
    private void OnThrow()
    {
        if (_gameStateController.currentState != _gameStateController.playState)
        {
            return;
        }
        if (Time.timeScale != 1)
        {
            return;
        }
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

        _camera.Rotaion();
    }

    private void OnRush()
    {
        audioSource.PlayOneShot(GoSE);
        mouseState = MouseState.rushTiku;
        foreach (GameObject c in callTikuminList)
        {
            c.GetComponent<ChikuminBase>().aiState = ChikuminBase.ChikuminAiState.ONRUSH;

        }
        callTikuminList.Clear();
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

    public int Step(int x)
    {
        return Convert.ToInt32(x % 2 == 0);
    }

    public void nCollisionEnter(Collision collision)
    {
        print(collision.gameObject.layer);
    }



}

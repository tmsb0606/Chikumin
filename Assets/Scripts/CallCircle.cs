using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallCircle : MonoBehaviour
{
    // Start is called before the first frame update

    public PlayerController player;
    public List<GameObject> chikuminList = new List<GameObject>();
    [SerializeField]private float offsetY = 90;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        transform.rotation = Quaternion.Euler(0, (getAngle(player.transform.position,transform.position) * -1) + offsetY , 0);
    }

    private float getAngle(Vector3 pos1 , Vector3 pos2)
    {
        
        float angle =  Mathf.Atan2(pos2.z - pos1.z, pos2.x - pos1.x) * Mathf.Rad2Deg;
       // print("角度" + angle);
        return angle;
    }
    public void OnTriggerStay(Collider other)
    {
        //print(LayerMask.LayerToName(other.gameObject.layer));
        if (!other.gameObject.GetComponent<ChikuminBase>())
        {
            return;
        }
       
        if (player.mouseState == PlayerController.MouseState.callTiku)
        {
            if (!other.gameObject.GetComponent<ChikuminBase>().canCall)
            {
                return;
            }



            //print(other.gameObject.name);
            other.gameObject.GetComponent<ChikuminBase>().aiState = ChikuminBase.ChikuminAiState.MOVE;
            other.gameObject.GetComponent<ChikuminBase>().isItem = false;
            if (other.gameObject.GetComponent<ChikuminBase>().carryObjectList.Count > 0)
            {
                other.gameObject.GetComponent<ChikuminBase>().carryObjectList[0].GetComponent<Rigidbody>().isKinematic = false;
                other.gameObject.GetComponent<ChikuminBase>().carryObjectList[0].GetComponent<Rigidbody>().useGravity = true;
                other.gameObject.GetComponent<ChikuminBase>().carryObjectList[0].GetComponent<Item>().carryObjects.Remove(other.gameObject);
                other.gameObject.GetComponent<ChikuminBase>().carryObjectList[0].transform.parent = null;
                //other.gameObject.GetComponent<ChikuminBase>().carryObjectList[0] = null;

                //後でこれを修正する
                other.gameObject.GetComponent<ChikuminBase>().carryObjectList.RemoveAt(0);

                //other.gameObject.GetComponent<ChikuminBase>().carryObjectList.Remove(other.gameObject.GetComponent<ChikuminBase>().carryObjectList[0]);

                //other.gameObject.GetComponent<ChikuminBase>().hitList.Remove(other.gameObject.GetComponent<ChikuminBase>().carryObjectList[0]);
            }
    
            if (!player.callTikuminList.Contains(other.gameObject))
            {
                player.callTikuminList.Add(other.gameObject);
            }
        }else if(player.mouseState == PlayerController.MouseState.waitTiku)
        {
            //print(other.gameObject.name);
            //全キャラ止めるならいらない
            //other.gameObject.GetComponent<ChikuminBase>().aiState = ChikuminBase.ChikuminAiState.IDLE;

            //player.callTikuminList.Remove(other.gameObject);

        }
    }



}

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
       // print("äpìx" + angle);
        return angle;
    }
    public void OnTriggerStay(Collider other)
    {
        //print(LayerMask.LayerToName(other.gameObject.layer));
        if (!other.gameObject.GetComponent<ChikuminBase>())
        {
            return;
        }
        ChikuminBase chikumin = other.GetComponent<ChikuminBase>();
       
        if (player.mouseState == PlayerController.MouseState.callTiku)
        {
            if (!chikumin.canCall)
            {
                return;
            }



            //print(other.gameObject.name);
            chikumin.aiState = ChikuminBase.ChikuminAiState.MOVE;
            chikumin.isItem = false;
            if (chikumin.carryObjectList.Count > 0)
            {
                chikumin.carryObjectList[0].GetComponent<Rigidbody>().isKinematic = false;
                chikumin.carryObjectList[0].GetComponent<Rigidbody>().useGravity = true;
                chikumin.carryObjectList[0].GetComponent<Item>().carryObjects.Remove(other.gameObject);
                chikumin.carryObjectList[0].transform.parent = null;

                //å„Ç≈Ç±ÇÍÇèCê≥Ç∑ÇÈ
                chikumin.carryObjectList.RemoveAt(0);

            }
    
            if (!player.callTikuminList.Contains(other.gameObject))
            {
                player.callTikuminList.Add(other.gameObject);
            }
        }
    }



}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallCircle : MonoBehaviour
{
    // Start is called before the first frame update

    public PlayerController player;
    public List<GameObject> chikuminList = new List<GameObject>();
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //transform.rotation = new Quaternion(0, Camera.main.transform.rotation.y, 0,this.transform.rotation.w);
        transform.rotation = Quaternion.Euler(90, Camera.main.transform.localEulerAngles.y, 0);
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

                //å„Ç≈Ç±ÇÍÇèCê≥Ç∑ÇÈ
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
            //ëSÉLÉÉÉâé~ÇﬂÇÈÇ»ÇÁÇ¢ÇÁÇ»Ç¢
            //other.gameObject.GetComponent<ChikuminBase>().aiState = ChikuminBase.ChikuminAiState.IDLE;

            //player.callTikuminList.Remove(other.gameObject);

        }
    }



}

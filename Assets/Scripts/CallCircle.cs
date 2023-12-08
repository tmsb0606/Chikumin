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
        
    }
    public void OnTriggerStay(Collider other)
    {
        if (!other.gameObject.GetComponent<ChikuminBase>())
        {
            return;
        }
       
        if (player.mouseState == PlayerController.MouseState.callTiku)
        {

            print(other.gameObject.name);
            other.gameObject.GetComponent<ChikuminBase>().aiState = ChikuminBase.ChikuminAiState.MOVE;
            if (!player.callTikuminList.Contains(other.gameObject))
            {
                player.callTikuminList.Add(other.gameObject);
            }
        }else if(player.mouseState == PlayerController.MouseState.waitTiku)
        {
            print(other.gameObject.name);
            other.gameObject.GetComponent<ChikuminBase>().aiState = ChikuminBase.ChikuminAiState.IDLE;

            player.callTikuminList.Remove(other.gameObject);

        }
    }



}

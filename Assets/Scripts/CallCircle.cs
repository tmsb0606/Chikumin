using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallCircle : MonoBehaviour
{
    // Start is called before the first frame update

    public PlayerController player;
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
        }else if(player.mouseState == PlayerController.MouseState.waitTiku)
        {
            print(other.gameObject.name);
            other.gameObject.GetComponent<ChikuminBase>().aiState = ChikuminBase.ChikuminAiState.WAIT;
        }
    }
    public void OnCollisionEnter(Collision collision)
    {

    }
}

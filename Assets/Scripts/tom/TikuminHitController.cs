using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TikuminHitController : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        //print("search”ÍˆÍ");
        if (other.gameObject.tag == "tiku")
        {
            transform.root.gameObject.GetComponent<PlayerController>().hitTikuminList.Add(other.gameObject);
            //print(other.gameObject.name);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        //print("search”ÍˆÍ");
        if (other.gameObject.tag == "tiku")
        {
            transform.root.gameObject.GetComponent<PlayerController>().hitTikuminList.Remove(other.gameObject);
            //print(other.gameObject.name);
        }
    }
}

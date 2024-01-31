using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oclusuon : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "tiku" || other.gameObject.tag == "item" || other.gameObject.tag == "enemy" || other.gameObject.tag == "circle"|| other.gameObject.tag == "atm"|| other.gameObject.tag == "ground"|| other.gameObject.tag == "WaitArea")
        {
            return;
        }
        Renderer r = other.gameObject.GetComponent<Renderer>();
        r.enabled = false;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player"|| other.gameObject.tag == "tiku"|| other.gameObject.tag == "item"|| other.gameObject.tag == "enemy"|| other.gameObject.tag == "circle" || other.gameObject.tag == "atm" || other.gameObject.tag == "ground" || other.gameObject.tag == "WaitArea")
        {
            return;
        }
        Renderer r = other.gameObject.GetComponent<Renderer>();
        r.enabled = true;
    }
}

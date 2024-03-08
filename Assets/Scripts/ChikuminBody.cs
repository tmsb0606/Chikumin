using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ChikuminBase;

public class ChikuminBody : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "AdachiArea" && transform.root.gameObject.GetComponent<ChikuminBase>().aiState == ChikuminAiState.ALIGNMENT)
        {
            transform.root.gameObject.GetComponent<ChikuminBase>().aiState = ChikuminAiState.WAIT;
        }
        if (other.gameObject.tag == "enemy")
        {
            transform.root.gameObject.GetComponent<ChikuminBase>().isHit = true;
        }

        if (other.gameObject.tag == "item"&& !transform.root.gameObject.GetComponent<ChikuminBase>().carryObjectList.Contains(other.gameObject))
        {
            transform.root.gameObject.GetComponent<ChikuminBase>().isItem = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "enemy")
        {
            transform.root.gameObject.GetComponent<ChikuminBase>().isHit = false;
        }
    }
}

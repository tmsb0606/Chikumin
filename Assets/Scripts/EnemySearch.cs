using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySearch : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        //print("search”ÍˆÍ");
        if (other.gameObject.tag == "Player"||other.gameObject.tag == "tiku")
        {
            if (transform.root.gameObject.GetComponent<EnemyController>().targetObjects.Contains(other.gameObject))
            {
                return;
            }
            transform.root.gameObject.GetComponent<EnemyController>().targetObjects.Add(other.gameObject);
            
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player"||other.gameObject.tag=="tiku")
        {
            transform.root.gameObject.GetComponent<EnemyController>().targetObjects.Remove(other.gameObject);
            
        }
    }
}

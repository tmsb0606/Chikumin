using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalController : MonoBehaviour
{
    public int score = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "item")
        {
            collision.gameObject.GetComponent<Item>().carryObjects[0].GetComponent<ChikuminBase>().carryObjectList[0] = null;
            collision.gameObject.transform.parent = null;
            collision.gameObject.SetActive(false);
            score += 100;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "item")
        {
            other.gameObject.GetComponent<Item>().carryObjects[0].GetComponent<ChikuminBase>().carryObjectList.Clear();
            other.gameObject.transform.parent = null;
            other.gameObject.SetActive(false);
            score += 100;
        }
    }
}

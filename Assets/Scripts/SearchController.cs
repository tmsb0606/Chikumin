using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchController : MonoBehaviour
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
        //print("search”ÍˆÍ");
        if (other.gameObject.tag != "ground" && other.gameObject.tag != "tiku"&& other.gameObject.tag != "circle")
        {
            transform.root.gameObject.GetComponent<ChikuminBase>().hitList.Add(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        //print("search”ÍˆÍ");
        if (other.gameObject.tag != "ground" && other.gameObject.tag != "tiku" && other.gameObject.tag != "circle")
        {
            transform.root.gameObject.GetComponent<ChikuminBase>().hitList.Remove(other.gameObject);
        }
    }
}

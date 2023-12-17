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
            collision.gameObject.GetComponent<Item>().carryObjects[0].GetComponent<Adachikumin>().carryObject = null;
            collision.gameObject.transform.parent = null;
            collision.gameObject.SetActive(false);
            score += 100;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBody : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //targetObject = other.gameObject;
        }
        if (other.gameObject.tag == "tiku")
        {
            if (other.gameObject.GetComponent<ChikuminBase>())
            {
                transform.root.gameObject.GetComponent<EnemyController>().isAttack = true;
                transform.root.gameObject.GetComponent<EnemyController>().attackTarget = other.gameObject;
            }
            
        }
    }
}

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
    private void OnTriggerEnter(Collider other)
    {
        //print("search”ÍˆÍ");
        if (other.gameObject.tag == "Player")
        {
            transform.root.gameObject.GetComponent<EnemyController>().targetObject = other.gameObject;
            transform.root.gameObject.GetComponent<EnemyController>().aiState = EnemyController.EnemyAiState.Chase;
        }
        print("test");

    }
}

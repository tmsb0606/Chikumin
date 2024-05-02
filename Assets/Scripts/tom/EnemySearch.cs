using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySearch : MonoBehaviour
{
    // Start is called before the first frame update
    private EnemyController enemyController;
    void Start()
    {
        enemyController = transform.root.gameObject.GetComponent<EnemyController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(enemyController.enabled == false)
        {
            this.gameObject.SetActive(false);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        //print("search”ÍˆÍ");
        if (other.gameObject.tag == "Player"||other.gameObject.tag == "tiku")
        {
            if (enemyController.targetObjects.Contains(other.gameObject))
            {
                return;
            }
            enemyController.targetObjects.Add(other.gameObject);
            
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player"||other.gameObject.tag=="tiku")
        {
            enemyController.targetObjects.Remove(other.gameObject);
            
        }
    }
}

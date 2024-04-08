using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBody : MonoBehaviour
{
    private EnemyController enemyController;
    void Start()
    {
        enemyController = transform.root.gameObject.GetComponent<EnemyController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyController.enabled == false)
        {
            this.gameObject.SetActive(false);
        }
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
                enemyController.isAttack = true;
                enemyController.attackTarget = other.gameObject;
            }
            
        }
    }
}

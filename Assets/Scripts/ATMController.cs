using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ATMController : MonoBehaviour
{
    public int maxNum=6;
    public int minNum=1;
    public GameObject moneyPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReleaseMoney()
    {
        int limit = Random.Range(minNum, maxNum);
        for (int i = 0; i <limit; i++)
        {
            print("ƒŠƒŠ[ƒX");
            GameObject money = Instantiate(moneyPrefab);
            money.transform.position = this.transform.position;
            Vector3 vec = new Vector3(100,100,100);
            money.GetComponent<Rigidbody>().AddForce(vec);
            //this.gameObject.SetActive(false);
            this.gameObject.tag = "Untagged";
        }
    }

}

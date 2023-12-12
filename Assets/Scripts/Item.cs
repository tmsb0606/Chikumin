using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour, ICarriable
{
    // Start is called before the first frame update
    public List<GameObject> carryObject = new List<GameObject>();
    public int minCarryNum = 1;
    public int maxCarryNum = 5;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Carry(GameObject gameObject)
    {

    }
}

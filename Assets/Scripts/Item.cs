using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour, ICarriable
{
    public enum Type
    {
        Wad,
        Jewelry,

    }
    // Start is called before the first frame update
    public Type itemType = Type.Wad;
    public List<GameObject> carryObjects = new List<GameObject>();
    public int minCarryNum = 1;
    public int maxCarryNum = 1;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Carry(GameObject gameObject)
    {
        carryObjects.Add(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Item : MonoBehaviour, ICarriable
{
    public enum Type
    {
        Wad,
        Jewelry,
        Rock,
        Branch,
        Car

    }
    // Start is called before the first frame update
    public Type itemType = Type.Wad;
    public List<GameObject> carryObjects = new List<GameObject>();
    public int minCarryNum = 1;
    public int maxCarryNum = 1;
    GameObject canvas;
    TextMeshProUGUI text;


    void Start()
    {
        canvas = Instantiate((GameObject)Resources.Load("Canvas"));
        
        canvas.transform.parent = this.transform;
        canvas.transform.localPosition = new Vector3(0, 0.5f, 0);

        //canvas = gameObject.transform.Find("Canvas").gameObject;
        text = canvas.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if(carryObjects.Count > 0)
        {
            canvas.SetActive(true);
            text.text = carryObjects.Count + "/" + minCarryNum;
        }

        if(canvas.active && carryObjects.Count == 0)
        {
            canvas.SetActive(false);
        }
    }
    public void Carry(GameObject gameObject)
    {
        carryObjects.Add(gameObject);
    }
}

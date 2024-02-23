using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class GoalController : MonoBehaviour
{
    public int score = 0;
    public int num = 10;
    //public int[] itemNum;
    public Dictionary<Item.Type, int> itemDic = new Dictionary<Item.Type, int>();
    private AudioSource audioSource;
    public AudioClip CoinSE;
    public ItemDataBase ItemDataBase;
    // Start is called before the first frame update
    void Start()
    {
        //itemNum = new int[num];
        //Array.Fill(itemNum, 0);
        audioSource = GameObject.Find("SoundDirector").GetComponent<AudioSource>();

        //取得アイテムリストを更新
        foreach (Item.Type Value in Enum.GetValues(typeof(Item.Type)))
        {
            itemDic.Add(Value, 0);
        }

    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "item")
        {
            collision.gameObject.GetComponent<Item>().carryObjects[0].GetComponent<ChikuminBase>().carryObjectList[0] = null;
            collision.gameObject.transform.parent = null;

            collision.gameObject.SetActive(false);
            IEnumerable<ItemData> item = ItemDataBase.itemList.Where(e => e.itemType == collision.gameObject.GetComponent<Item>().itemType);
            score += item.First().money;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "item")
        {
            other.gameObject.GetComponent<Item>().carryObjects[0].GetComponent<ChikuminBase>().carryObjectList.Clear();
            //itemNum[(int)other.gameObject.GetComponent<Item>().itemType] += 1;
            itemDic[other.gameObject.GetComponent<Item>().itemType] += 1;
            print(other.gameObject.GetComponent<Item>().itemType + ":" + itemDic[other.gameObject.GetComponent<Item>().itemType]);
            other.gameObject.transform.parent = null;
            other.gameObject.SetActive(false);
            IEnumerable<ItemData> item = ItemDataBase.itemList.Where(e => e != null).Where(e => e.itemType == other.gameObject.GetComponent<Item>().itemType);
            print("itemtype:"+item.First().itemType);
            score += item.First().money;
            audioSource.PlayOneShot(CoinSE);
        }
    }
}

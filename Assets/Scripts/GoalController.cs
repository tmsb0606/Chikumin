using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using Cysharp.Threading.Tasks;

public class GoalController : MonoBehaviour
{
    public int score = 0;
    public int num = 10;
    //public int[] itemNum;
    public Dictionary<Item.Type, int> itemDic = new Dictionary<Item.Type, int>();
    private AudioSource audioSource;
    public AudioClip CoinSE;
    public AudioClip GetSE;
    public ItemDataBase ItemDataBase;

    private List<GameObject> goalItemList = new List<GameObject>();
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
            
/*            other.gameObject.GetComponent<Item>().carryObjects[0].GetComponent<ChikuminBase>().carryObjectList.Clear();
            //itemNum[(int)other.gameObject.GetComponent<Item>().itemType] += 1;
            itemDic[other.gameObject.GetComponent<Item>().itemType] += 1;
            print(other.gameObject.GetComponent<Item>().itemType + ":" + itemDic[other.gameObject.GetComponent<Item>().itemType]);
            other.gameObject.transform.parent = null;
            other.gameObject.SetActive(false);
            IEnumerable<ItemData> item = ItemDataBase.itemList.Where(e => e != null).Where(e => e.itemType == other.gameObject.GetComponent<Item>().itemType);
            print("itemtype:"+item.First().itemType);
            score += item.First().money;
            audioSource.PlayOneShot(CoinSE);*/
        }

        

        if (other.gameObject.tag == "tiku")
        {
            ItemCalculation(other);

/*            if (other.gameObject.GetComponent<ChikuminBase>())
            {
                if(other.gameObject.GetComponent<ChikuminBase>().carryObjectList.Count != 0)
                {
                    other.gameObject.GetComponent<ChikuminBase>().aiState = ChikuminBase.ChikuminAiState.WAIT;
                }
                foreach (GameObject obj in other.gameObject.GetComponent<ChikuminBase>().carryObjectList)
                {
                    print("goalobj"+obj);
                    obj.gameObject.GetComponent<Item>().Absorbed();
                    itemDic[obj.gameObject.GetComponent<Item>().itemType] += 1;
                    print(obj.gameObject.GetComponent<Item>().itemType + ":" + itemDic[obj.gameObject.GetComponent<Item>().itemType]);
                    obj.gameObject.transform.parent = null;
                    foreach(GameObject tiku in obj.gameObject.GetComponent<Item>().carryObjects)
                    {
                        tiku.gameObject.GetComponent<ChikuminBase>().carryObjectList = new List<GameObject>();
                    }
                    //obj.gameObject.SetActive(false);
                    IEnumerable<ItemData> item = ItemDataBase.itemList.Where(e => e != null).Where(e => e.itemType == obj.gameObject.GetComponent<Item>().itemType);
                    print("itemtype:" + item.First().itemType);
                    score += item.First().money;
                    audioSource.PlayOneShot(CoinSE);
                }

                other.GetComponent<ChikuminBase>().carryObjectList.Clear();
                
            }*/
        }
    }

    async void ItemCalculation(Collider other)
    {
        print("オブジェクト"+other.gameObject.name);
        if (!other.gameObject.GetComponent<ChikuminBase>())
        {
            return;
        }
        if (other.gameObject.GetComponent<ChikuminBase>().carryObjectList.Count != 0)
        {
            other.gameObject.GetComponent<ChikuminBase>().aiState = ChikuminBase.ChikuminAiState.WAIT;
        }
        other.gameObject.GetComponent<ChikuminBase>().canCall = false;
        List<GameObject> objlist = new List<GameObject>();
        foreach (GameObject obj in other.gameObject.GetComponent<ChikuminBase>().carryObjectList)
        {
            if (!goalItemList.Contains(obj))
            {
                goalItemList.Add(obj);
                objlist.Add(obj);
                obj.transform.parent = null;
            }
        }
        other.GetComponent<ChikuminBase>().carryObjectList.Clear();
        other.gameObject.GetComponent<ChikuminBase>().canCall = true;


        foreach (GameObject obj in objlist)
        {
            audioSource.PlayOneShot(GetSE);
            var isGoal = await obj.gameObject.GetComponent<Item>().Animation();
            //await UniTask.Delay(1000);

            itemDic[obj.gameObject.GetComponent<Item>().itemType] += 1;
            print(obj.gameObject.GetComponent<Item>().itemType + ":" + itemDic[obj.gameObject.GetComponent<Item>().itemType]);
            obj.gameObject.transform.parent = null;
            foreach (GameObject tiku in obj.gameObject.GetComponent<Item>().carryObjects)
            {
                tiku.gameObject.GetComponent<ChikuminBase>().carryObjectList = new List<GameObject>();
            }
            obj.gameObject.SetActive(false);
            IEnumerable<ItemData> item = ItemDataBase.itemList.Where(e => e != null).Where(e => e.itemType == obj.gameObject.GetComponent<Item>().itemType);
            print("itemtype:" + item.First().itemType);
            score += item.First().money;
            audioSource.PlayOneShot(CoinSE);

        }





    }


}

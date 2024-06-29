using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using Cysharp.Threading.Tasks;

public class GoalController : MonoBehaviour
{
    
    public int num = 10;
    //public int[] itemNum;
    private AudioSource audioSource;
    public AudioClip CoinSE;
    public AudioClip GetSE;
    public ItemDataBase ItemDataBase;

    private List<GameObject> goalItemList = new List<GameObject>();

    [SerializeField] private Canvas _canvas;
    [SerializeField] private GameObject _scoreUpPrefab;
    [SerializeField] private Vector3 _scoreUpTextPos;
    // Start is called before the first frame update
    void Start()
    {
        //itemNum = new int[num];
        //Array.Fill(itemNum, 0);
        audioSource = GameObject.Find("SoundDirector").GetComponent<SoundDirector>().seSource;

        //�擾�A�C�e�����X�g���X�V
        foreach (Item.Type Value in Enum.GetValues(typeof(Item.Type)))
        {
           // itemDic.Add(Value, 0);
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
            ScoreDirector.score += item.First().money;
        }
    }
    private void OnTriggerEnter(Collider other)
    {

        

        if (other.gameObject.tag == "tiku")
        {
            ItemCalculation(other);
        }
    }

    async void ItemCalculation(Collider other)
    {
        print("�I�u�W�F�N�g"+other.gameObject.name);
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
            obj.gameObject.GetComponent<Item>().Effect();
            //await UniTask.Delay(1000);

            ScoreDirector.itemDic[obj.gameObject.GetComponent<Item>().itemType] += 1;
            print(obj.gameObject.GetComponent<Item>().itemType + ":" + ScoreDirector.itemDic[obj.gameObject.GetComponent<Item>().itemType]);
            obj.gameObject.transform.parent = null;
            foreach (GameObject tiku in obj.gameObject.GetComponent<Item>().carryObjects)
            {
                tiku.gameObject.GetComponent<ChikuminBase>().carryObjectList = new List<GameObject>();
            }
            obj.gameObject.SetActive(false);
            IEnumerable<ItemData> item = ItemDataBase.itemList.Where(e => e != null).Where(e => e.itemType == obj.gameObject.GetComponent<Item>().itemType);
            print("itemtype:" + item.First().itemType);
            ScoreDirector.score += item.First().money;
            GameObject scoretext =  Instantiate(_scoreUpPrefab, _canvas.transform);
            ScoreUpAnim scoreAnimation = scoretext.GetComponent<ScoreUpAnim>();
            scoretext.transform.localPosition = _scoreUpTextPos;
            scoreAnimation.setText(item.First().money);
            scoreAnimation.TakeScoreAnim();
            
            audioSource.PlayOneShot(CoinSE);

        }





    }


}
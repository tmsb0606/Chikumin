using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine.UI;
using System;
public class ResultPanelController : MonoBehaviour
{
    [SerializeField] private GameObject _resultContent;
    [SerializeField] private GameObject _itemLinePrefab;
    [SerializeField] private ItemDataBase _itemDataBase;
    [SerializeField] private GoalController _goalController;
    [SerializeField] private TextMeshProUGUI _totalScoreText;
    [SerializeField] private ScrollRect _scrollRect;
    // Start is called before the first frame update
    void Start()
    {
        _totalScoreText.text = "0";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public async UniTask<bool> CreateResultPanel()
    {
        foreach (ItemData item in  _itemDataBase.itemList)
        {


            print(item.itemType);
            if ( ScoreDirector.itemDic[item.itemType] != 0)
            {
                GameObject obj = Instantiate( _itemLinePrefab,  _resultContent.transform);
                obj.transform.Find("NumberOfItems").GetComponent<TextMeshProUGUI>().text = ScoreDirector.itemDic[item.itemType].ToString();
                obj.transform.Find("ItemMoneyText").GetComponent<TextMeshProUGUI>().text = (ScoreDirector.itemDic[item.itemType] * item.money).ToString();
                obj.transform.Find("ItemImage").GetComponent<Image>().sprite = item.itemImage;
                _scrollRect.velocity = new Vector2(0, 1000f);

                //アニメーションが終わるまで待機するように書き換える。
                await UniTask.Delay(TimeSpan.FromSeconds(0.5f));
                


            }

        }
        _totalScoreText.text = ScoreDirector.score.ToString("0");
        return true;
    }
}

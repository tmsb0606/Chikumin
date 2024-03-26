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
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip itemSE;
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

                _audioSource.PlayOneShot(itemSE);
                _scrollRect.velocity = new Vector2(0, 1000f);
                await obj.GetComponent<ResultAnim>().AsyncItemAnim();
                
                


            }

        }
        await UniTask.Delay(500);
        _audioSource.PlayOneShot(itemSE);
        _totalScoreText.text = ScoreDirector.score.ToString("0");
        return true;
    }
}

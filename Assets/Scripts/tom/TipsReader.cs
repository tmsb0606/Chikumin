using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class TipsReader : MonoBehaviour
{
    TextAsset csvFile; // CSVファイル
    public List<string[]> csvDatas = new List<string[]>(); // CSVの中身を入れるリスト;

    private TipsController _tipsController;

    

    void Start()
    {
        _tipsController = GetComponent<TipsController>();
        
        Addressables.LoadAssetAsync<TextAsset>("Tips").Completed += handle =>
        {
            print("csvロード");
            if(handle.Result == null)
            {
                print("ロードエラー");
                return;
            }
            print(handle.Result.text);
            StringReader reader = new StringReader(handle.Result.text);
            reader.ReadLine();
            // , で分割しつつ一行ずつ読み込み
            // リストに追加していく
            while (reader.Peek() != -1) // reader.Peaekが-1になるまで
            {
                string line = reader.ReadLine(); // 一行ずつ読み込み
                TipsData.csvDatas.Add(line.Split(',')); // , 区切りでリストに追加
            }

            // csvDatas[行][列]を指定して値を自由に取り出せる
            Debug.Log(TipsData.csvDatas[0][2]);
           // _tipsController.ShowTips();

        };
        /*print(reader.ReadLine());
        csvFile = Resources.Load("Tips") as TextAsset; // Resouces下のCSV読み込み
                                                       //StringReader reader = new StringReader(csvFile.text);
        reader.ReadLine();
        // , で分割しつつ一行ずつ読み込み
        // リストに追加していく
        while (reader.Peek() != -1) // reader.Peaekが-1になるまで
        {
            string line = reader.ReadLine(); // 一行ずつ読み込み
            csvDatas.Add(line.Split(',')); // , 区切りでリストに追加
        }

        // csvDatas[行][列]を指定して値を自由に取り出せる
        Debug.Log(csvDatas[0][2]);*/

    }



}

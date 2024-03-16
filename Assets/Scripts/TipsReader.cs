using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class TipsReader : MonoBehaviour
{
    TextAsset csvFile; // CSV�t�@�C��
    public List<string[]> csvDatas = new List<string[]>(); // CSV�̒��g�����郊�X�g;

    private TipsController _tipsController;

    

    void Start()
    {
        _tipsController = GetComponent<TipsController>();
        
        Addressables.LoadAssetAsync<TextAsset>("Tips").Completed += handle =>
        {
            print("csv���[�h");
            if(handle.Result == null)
            {
                print("���[�h�G���[");
                return;
            }
            print(handle.Result.text);
            StringReader reader = new StringReader(handle.Result.text);
            reader.ReadLine();
            // , �ŕ�������s���ǂݍ���
            // ���X�g�ɒǉ����Ă���
            while (reader.Peek() != -1) // reader.Peaek��-1�ɂȂ�܂�
            {
                string line = reader.ReadLine(); // ��s���ǂݍ���
                TipsData.csvDatas.Add(line.Split(',')); // , ��؂�Ń��X�g�ɒǉ�
            }

            // csvDatas[�s][��]���w�肵�Ēl�����R�Ɏ��o����
            Debug.Log(TipsData.csvDatas[0][2]);
           // _tipsController.ShowTips();

        };
        /*print(reader.ReadLine());
        csvFile = Resources.Load("Tips") as TextAsset; // Resouces����CSV�ǂݍ���
                                                       //StringReader reader = new StringReader(csvFile.text);
        reader.ReadLine();
        // , �ŕ�������s���ǂݍ���
        // ���X�g�ɒǉ����Ă���
        while (reader.Peek() != -1) // reader.Peaek��-1�ɂȂ�܂�
        {
            string line = reader.ReadLine(); // ��s���ǂݍ���
            csvDatas.Add(line.Split(',')); // , ��؂�Ń��X�g�ɒǉ�
        }

        // csvDatas[�s][��]���w�肵�Ēl�����R�Ɏ��o����
        Debug.Log(csvDatas[0][2]);*/

    }



}

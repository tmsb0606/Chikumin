using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;


public class EventTest : MonoBehaviour
{
    [SerializeField] private Image m_Image;

    private AsyncOperationHandle<Sprite> m_SpriteHandle;
    // Start is called before the first frame update
    void Start()
    {
        Addressables.LoadAssetAsync<Sprite>("CHIKMIN_Rogo").Completed += handle => {
            m_SpriteHandle = handle;
            if (handle.Result == null)
            {
                Debug.Log("Load Error");
                return;
            }
            m_Image.sprite = handle.Result;
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Test(string test)
    {
        print(test);
    }

    private void OnDestroy()
    {
        if (m_SpriteHandle.IsValid()) Addressables.Release(m_SpriteHandle);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEffectGetChikumin : ItemEffectBase
{
    private GameObject ChikuminPrefab;
    private void Start()
    {
        ChikuminPrefab = (GameObject)Resources.Load("Chikumin");
    }
    public override void Effect()
    {
        print("チクミンゲット");
        GameObject obj = Instantiate(ChikuminPrefab);
        obj.GetComponent<IJampable>().Jump(obj.transform.position, 1f, 5f, 9.8f);
    }

}

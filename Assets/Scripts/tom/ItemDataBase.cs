using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "ItemDataBase", menuName = "ScriptableObjects/CreateItemDataBase")]
public class ItemDataBase : ScriptableObject
{
    public List<ItemData> itemList = new List<ItemData>() ;
}

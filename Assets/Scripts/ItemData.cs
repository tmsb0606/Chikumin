using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "ScriptableObjects/CreateItemData")]
public class ItemData : ScriptableObject
{
    public Sprite itemImage;
    public Item.Type itemType;
    public int money;
}

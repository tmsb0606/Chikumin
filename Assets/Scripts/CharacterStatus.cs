using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Staus", menuName = "ScriptableObjects/CreateStatus")]
public class CharacterStatus : ScriptableObject
{
    public float moveSpeed = 0;
    public int level = 1;
}

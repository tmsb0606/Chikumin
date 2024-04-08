using UnityEngine;

public static class TransformExtensions
{
    public static void DestroyAllChildren(this Transform parent)
    {
        foreach (Transform child in parent)
        {
            child.gameObject.SetActive(false);
        }
    }
}
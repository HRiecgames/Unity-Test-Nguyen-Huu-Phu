using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Item View Collection", menuName = "Scriptable Objects/Databases/Item View Collection")]
public class ItemViewCollection : ScriptableObject
{
    [SerializeField] private List<ItemViewData> itemViewDatas;

    public Dictionary<string, Sprite> ViewCollection;

    public void Initialize()
    {
        ViewCollection = itemViewDatas.ToDictionary(key => key.viewName, value => value.viewSprite);
    }

    public Sprite GetSpriteByName(string viewName)
    {
        return ViewCollection.TryGetValue(viewName, out var view) ? view : null;
    }
}

[Serializable]
public struct ItemViewData
{
    public string viewName;
    public Sprite viewSprite;
}

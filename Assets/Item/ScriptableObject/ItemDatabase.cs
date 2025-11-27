using UnityEngine;
using System.Collections.Generic;
[CreateAssetMenu(fileName = "ItemDatabase",menuName = "Inventory/Item Database")]
public class ItemDatabase : ScriptableObject
{
    public static ItemDatabase Instance;
   public List<ItemData> allItems;

   public ItemData GetItemByID(string id)
    {
        return allItems.Find(item => item.itemID == id);
    }
}

using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class InventoryManager : MonoBehaviour,IDataPresistence
{
    public static InventoryManager Instance;

    public List<ItemData> Items = new List<ItemData>();
    public Transform[] Slots;
    public GameObject ItemPrefab;

    void Awake()
    {
        Instance = this;
    }

    public void AddItem(ItemData data)
    {
        Items.Add(data);
        UpdateUI();
    }

    public void RemoveItem(int index)
    {
        if (index < 0 || index >= Items.Count)
        return;
        Items.RemoveAt(index);
        UpdateUI();
    }

    private void UpdateUI()
    {
        foreach (Transform slot in Slots)
        {
            if(slot.childCount > 0)
            Destroy(slot.GetChild(0).gameObject);
        }
        for (int i = 0; i < Items.Count && i < Slots.Length; i++)
        {
            var uiObject = Instantiate(ItemPrefab, Slots[i]);
            var ui = uiObject.GetComponent<ItemUI>();
            
            ui.SetUI(Items[i]);
        }
    }

    public void SaveData(ref GameData data)
    {
       data.InventoryItemIDs.Clear();
       foreach (var item in Items)
        {
            data.InventoryItemIDs.Add(item.itemID);
        }
    }

    public void LoadData(GameData data)
    {
        Items.Clear();
        foreach(string id in data.InventoryItemIDs)
        {
            var ItemData = ItemDatabase.Instance.GetItemByID(id);

            if (ItemData != null)
            Items.Add(ItemData);
        }
        UpdateUI();
    }   
}

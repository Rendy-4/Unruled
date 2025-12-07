using UnityEngine;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour,IDataPresistence
{
    public static InventoryManager Instance;

    public List<ItemData> Items = new List<ItemData>();
    public List<string> collectedItemIDs = new List<string>();

    public Transform[] Slots;
    public GameObject ItemPrefab;
    public ItemDatabase database;


    void Awake()
    {
        Instance = this;
    }

    public void AddItem(ItemData data)
    {
        Items.Add(data);

        if(!collectedItemIDs.Contains(data.itemID))
        collectedItemIDs.Add(data.itemID);

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
       data.InventoryItemIDs.Clear(); //Apa Saja Item Yang Player Bawa
       foreach (var item in Items)
        data.InventoryItemIDs.Add(item.itemID);

        data.collectedItemIDs.Clear(); //Apa Saja Item Yang Pernah Diambil Player
        foreach(var id in collectedItemIDs)
        data.collectedItemIDs.Add(id);
    }

    public void LoadData(GameData data)
    {
        Items.Clear();
        foreach(string id in data.InventoryItemIDs)
        {
            var ItemData = database.GetItemByID(id);
            if (ItemData != null)
                Items.Add(ItemData);
        }

        collectedItemIDs.Clear();
        foreach(string id in data.collectedItemIDs)
            collectedItemIDs.Add(id);

        UpdateUI();
    }   
}

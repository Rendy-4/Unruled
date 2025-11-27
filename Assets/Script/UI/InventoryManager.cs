using UnityEngine;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    public List<string> Items = new List<string>();
    public Transform[] Slots;
    public GameObject ItemPrefab;

    void Awake()
    {
        Instance = this;
    }

    public void AddItem(string itemname)
    {
        Items.Add(itemname);
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
        for (int i = 0; i < Items.Count; i++)
        {
            GameObject newItem = Instantiate(ItemPrefab, Slots[i]);
            newItem.GetComponent<ItemUI>().SetItem(Items[i]);
            
        }
    }

}

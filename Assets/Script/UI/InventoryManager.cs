using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public List<string> Itemlist = new List<string>();
    public Transform InventoryBar;
    public GameObject slotPrefab;

    void Awake()
    {
        Instance = this;
    }

    public void AddItem(string itemname)
    {
        Itemlist.Add(itemname);
        UpdateUI();
    }

    private void UpdateUI()
    {
        foreach (Transform child in InventoryBar)
            Destroy(child.gameObject);
        
        foreach(string item in Itemlist)
        {
            GameObject slot = Instantiate(slotPrefab, InventoryBar);
            slot.GetComponentInChildren<Text>();
        }
    }

    public void Ambilitem()
    {
        
    }

}

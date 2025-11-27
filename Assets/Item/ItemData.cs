using UnityEngine;

[CreateAssetMenu(fileName = "ItemData",menuName = "Inventory/ItemData")]
public class ItemData : ScriptableObject
{
    public string itemID;
    public string ItemName;
    public Sprite icon;
}

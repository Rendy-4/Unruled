using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
   public string itemName;
   public Image icon;

   public void SetUI(ItemData data)
    {
        itemName = data.ItemName;
        icon.sprite = data.icon;

    }
}

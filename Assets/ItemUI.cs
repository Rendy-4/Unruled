using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
   public string itemName;
   public Image icon;

   public void SetItem(string name)
    {
        itemName = name;

    }
}

using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public string requiredItem;
    public void OnItemUsed(string itemname)
    {
        if (itemname == requiredItem)
        {
            Debug.Log("Item Cocok! oobjek diaktifkan.");
        }
        else
        {
            Debug.Log("Item tidak cocok!.");
        }
    }
}

using UnityEngine;

public class Interactable : MonoBehaviour
{
    public GameObject gambarInteract;
    public bool IsPlayerNear = false;

    private void Start()
    {
        gambarInteract.SetActive(false);
    }
    void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
        IsPlayerNear = true;
        gambarInteract.SetActive(true); 
        }
         
    }
    void OnTriggerExit(Collider collision)
    {
        if(collision.CompareTag("Player"))
        {
        IsPlayerNear = false;
        gambarInteract.SetActive(false);
        }
        
    }
    public void HideIcon()
    {
        gambarInteract.SetActive(false);
    }
    public void ShowIcon()
    {
        if (IsPlayerNear)
        {
            gambarInteract.SetActive(true);
        }
    }
    public bool isPlayerNear => IsPlayerNear;
}

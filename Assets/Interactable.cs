using UnityEngine;

public class Interactable : MonoBehaviour
{
    public GameObject gambarInteract;
    public MissionUIController Mission;
    
    private void Start()
    {
        gambarInteract.SetActive(false);
    }
    void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
        Mission.ShowMission("Find Alice In The Canteen");
        gambarInteract.SetActive(true); 
        } 
    }
    void OnTriggerExit(Collider collision)
    {
        if(collision.CompareTag("Player"))
        {
        gambarInteract.SetActive(false);
        }   
    }
    
}

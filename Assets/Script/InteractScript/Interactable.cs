using System;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [Header("Icon dsplay")]
    public GameObject gambarInteract;
    [Header("Mission")]
    public MissionUIController Mission;
    public String missionText;
    private bool SudahSelesai;
    
    private void Start()
    {
        if(gambarInteract != null)
        gambarInteract.SetActive(false);
    }
    void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player") && !SudahSelesai)
        {

        if (Mission != null)
        Mission.ShowMission(missionText);
        

        if (gambarInteract != null)
        gambarInteract.SetActive(true); 
        } 
        SudahSelesai = true;
    }

    void OnTriggerExit(Collider collision)
    {
        if(collision.CompareTag("Player"))
        {

        if(gambarInteract != null)
        gambarInteract.SetActive(false);
        }   

    }

    
    
}

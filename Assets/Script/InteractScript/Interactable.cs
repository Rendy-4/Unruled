using UnityEngine.UI;
using System;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [Header("Icon dsplay")]
    public GameObject gambarInteract;
    public Image iconimage;
    public Sprite iconSprite;


    [Header("Mission")]
    public MissionUIController Mission;
    public String missionText;
    private bool SudahSelesai;
    public int missionOrder;
    
    private void Start()
    {
        if(gambarInteract != null)
        gambarInteract.SetActive(false);
    }
    void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player") && !SudahSelesai)
        {
            bool valid = MissionManager.Instance.ValidateMission(missionOrder);
        if(!valid)
        return;

        if (Mission != null)
        Mission.ShowMission(missionText);
        if (iconimage != null && iconSprite != null)
        iconimage.sprite = iconSprite;
        
        if (gambarInteract != null)
        gambarInteract.SetActive(true); 
        SudahSelesai = true;
        } 
        
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

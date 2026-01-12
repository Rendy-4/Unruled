using UnityEngine;
using System;

public class MissionManager : MonoBehaviour , IDataPresistence
{
   public static MissionManager Instance;
   public static Action OnMissionUpdated;
   public static Action ForceRefreshNPC;
   public int currentMission;
   

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    

   public bool ValidateMission(int missionOrder)
{

    if (missionOrder == currentMission)
    {
        currentMission++;;
        OnMissionUpdated?.Invoke();
        ForceRefreshNPC?.Invoke();
        DataPresistenceManager.instance.SaveGame();
        return true;
    }

    return false;
}

    void HandleDialogueFinished(DialogueData data)
    {
        if (data == null)
        return;

        if(data.completeMission >= 0)
        {
            ValidateMission(data.completeMission);
        }
    }

    void OnEnable()
    {
        DialogueManager.onDialogueFinished += HandleDialogueFinished;
    }
    void OnDisable()
    {
        DialogueManager.onDialogueFinished -= HandleDialogueFinished;
    }

    public void LoadData(GameData data)
{
    currentMission = data.MissionOrder;
    Debug.Log($"LOAD MISSION: Mission={currentMission}");
    OnMissionUpdated?.Invoke();
}


    public void SaveData(ref GameData data)
    {
       data.MissionOrder = currentMission;
       
    }
}

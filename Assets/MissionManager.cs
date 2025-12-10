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
        Instance = this;
    }

    void Start()
    {
        OnMissionUpdated?.Invoke();
    }

   public bool ValidateMission(int missionOrder)
{

    if (missionOrder == currentMission)
    {
        currentMission++;;
        OnMissionUpdated?.Invoke();
        ForceRefreshNPC?.Invoke();
        return true;
    }

    return false;
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

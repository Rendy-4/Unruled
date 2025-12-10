using UnityEngine;

public class MissionManager : MonoBehaviour , IDataPresistence
{
   public static MissionManager Instance;
   public int currentMission;

    private void Awake()
    {
        Instance = this;
    }

    public bool ValidateMission(int missionOrder)
    {
        if(missionOrder == currentMission)
        {
            currentMission++;
            return true;
        }
        
        return false;
    }

    public void LoadData(GameData data)
    {
       currentMission = data.MissionOrder;
    }

    public void SaveData(ref GameData data)
    {
       data.MissionOrder = currentMission;
       
    }
}

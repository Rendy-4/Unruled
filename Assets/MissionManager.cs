using UnityEngine;

public class MissionManager : MonoBehaviour
{
   public static MissionManager Instance;
   public int currentMission = 0;

    private void Awake()
    {
        Instance = this;
    }

    public bool ValidateMission(int missionOrder)
    {
        if(missionOrder == currentMission + 1)
        {
            currentMission++;
            return true;
        }
        
        return false;
    }
}

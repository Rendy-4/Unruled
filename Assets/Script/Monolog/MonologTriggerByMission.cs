using UnityEngine;

public class MonologTriggerByMission : MonoBehaviour
{
    public int targetMissionID;
    public MonologUI monologUI;

    [TextArea(3, 6)]
    public string monologText;
    
    bool hasPlayed;

    void OnEnable()
    {
        MissionManager.OnMissionUpdated += CheckMission;
    }
    void OnDisable()
    {
        MissionManager.OnMissionUpdated -= CheckMission;
    }

    void CheckMission()
    {
        if (MissionManager.Instance.currentMission != targetMissionID)
        return;

        if(monologUI == null)
        {
            Debug.LogWarning("MonologUI belum di Assign",this);
            return;
        }
        monologUI.Play(monologText);
        hasPlayed = true;
       
    }
}

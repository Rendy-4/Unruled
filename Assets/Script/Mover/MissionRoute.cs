using UnityEngine;

[System.Serializable]
public class MissionRoute
{
    public int requiredMission;
    public Transform[] waypoints;
    public bool PlayOnce = true;
    
    [HideInInspector]
    public bool used;
}
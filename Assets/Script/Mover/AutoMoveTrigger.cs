using UnityEngine;
using System.Collections.Generic;

public class AutoMoveTrigger : MonoBehaviour
{
   public PlayerMover playerMover;
   public PlayerWaypointMission[] playerWaypoints;

    private void OnTriggerEnter(Collider other)
    {
        if(!other.CompareTag("Player"))
        return;

        int currentMission = MissionManager.Instance.currentMission;
        List<Transform> validWaypoints = new List<Transform>();

        foreach (var wp in playerWaypoints)
        {
            if(wp.requiredMission == currentMission)
            {
                validWaypoints.Add(wp.waypoint);
            }
        }   
        if (validWaypoints.Count > 0)
        {
            playerMover.StartMove(validWaypoints.ToArray());

        }
        
    }
    [System.Serializable]
   public class PlayerWaypointMission
    {
        public int requiredMission;
        public Transform waypoint;
    }
}

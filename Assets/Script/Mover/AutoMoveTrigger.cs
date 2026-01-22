using UnityEngine;

public class AutoMoveTrigger : MonoBehaviour
{
   public PlayerMover playerMover;
   public Transform[] playerWaypoints;

    private void OnTriggerEnter(Collider other)
    {
        if(!other.CompareTag("Player"))
        return;
        if(playerMover == null)
        return;
        playerMover.StartMove(playerWaypoints);

    }
}

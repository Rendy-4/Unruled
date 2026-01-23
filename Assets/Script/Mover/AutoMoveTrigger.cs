using UnityEngine;

public class AutoMoveTrigger : MonoBehaviour
{
   public PlayerMover playerMover;
   public Transform[] playerWaypoints;

    private void OnTriggerEnter(Collider other)
    {
        if(!other.CompareTag("player"))
        return;
        playerMover.StartMove(playerWaypoints);

    }
}

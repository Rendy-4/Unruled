using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class GameData 
{
    public Vector3 playerPosistion;
    public float musicVolume = 0.5f;
    public float sfxVolume = 0.5f;
    public int resolutionIndex;
    public bool isFullscreen;
    public int MissionOrder = 0;
    public List<string> InventoryItemIDs = new List<string>();
    public List<string> collectedItemIDs = new List<string>();
    public bool HasStartedGame = false;
    public GameData()
    {
        playerPosistion = new Vector3(497f, 0.71f, 30.172f);
        MissionOrder = 0;
       
    }
}

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
        playerPosistion = new Vector3(734.3f, 0.69f, -33.37f);
        MissionOrder = 0;
       
    }
}

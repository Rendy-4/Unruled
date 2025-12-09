using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class GameData 
{
    public Vector3 playerPosistion;
    public float musicVolume;
    public float sfxVolume;
    public int resolutionIndex;
    public bool isFullscreen;
    public int MissionOrder = 0;
    public List<string> InventoryItemIDs = new List<string>();
    public List<string> collectedItemIDs = new List<string>();
    public GameData()
    {
        playerPosistion = new Vector3(4.26f, 0.71f, -1.341f);
        MissionOrder = 0;
    }
}

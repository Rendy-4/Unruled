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
    public List<string> InventoryItemIDs = new List<string>();
    public GameData()
    {
        playerPosistion = new Vector3(3f, 1f, -0.45f);
    }
}

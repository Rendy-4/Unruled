using UnityEngine;
using System.Collections.Generic;
[System.Serializable]
public class NPCData
{
    public string npcID;       // unique ID tiap NPC
    public Vector3 position;
    public int lastMission;
    public bool visible;

    public NPCData(string id, Vector3 pos, int mission, bool visible)
    {
        this.npcID = id;
        this.position = pos;
        this.lastMission = mission;
        this.visible = visible;
    }
}

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
    public List<NPCData> npcs = new List<NPCData>();
    public GameData()
    {
        playerPosistion = new Vector3(870f, 1.38f, -33.52f);
        MissionOrder = 0;
       
    }
}

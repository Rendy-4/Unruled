using UnityEngine;

public class NPCSaveData : MonoBehaviour, IDataPresistence
{
    [Header("Unique NPC ID")]
    public string npcID; // unik untuk NPC ini

    private NpcMissionController missionController;
    private NPCMover npcMover;
    private Renderer[] renderers;
    private Collider npcCollider;

    private void Awake()
    {
        missionController = GetComponent<NpcMissionController>();
        npcMover = GetComponent<NPCMover>();
        renderers = GetComponentsInChildren<Renderer>();
        npcCollider = GetComponent<Collider>();
    }

    public void LoadData(GameData data)
    {
        var savedNPC = data.npcs.Find(n => n.npcID == npcID);
        if (savedNPC != null)
        {
            transform.position = savedNPC.position;
            if(missionController != null)
                missionController.SetState(savedNPC.lastMission, savedNPC.visible);

            // set visibilitas
            foreach(var r in renderers)
                r.enabled = savedNPC.visible;
            if(npcCollider != null)
                npcCollider.enabled = savedNPC.visible;
        }
    }

    public void SaveData(ref GameData data)
    {
        var existing = data.npcs.Find(n => n.npcID == npcID);
        if(existing != null)
            data.npcs.Remove(existing);

        int lastMission = npcMover != null ? npcMover.GetLastPlayedMission() : 0;
        bool visible = missionController != null ? missionController.IsVisible() : true;

        data.npcs.Add(new NPCData(npcID, transform.position, lastMission, visible));
    }
    
}

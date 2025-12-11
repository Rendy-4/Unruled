using UnityEngine;
using System.Collections.Generic;

public class CutsceneTrigger : MonoBehaviour
{
    public int missionOrderRequired = 0;
    public List<CutsceneLine> lines;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CutsceneManager.Instance.StartCutscene(lines, missionOrderRequired);
        }
    }
}

using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class CutsceneManager : MonoBehaviour
{
    public static CutsceneManager Instance;

    [Header("UI Reference")]
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;

    [Header("Typewriter Settings")]
    public float typeSpeed = 0.03f;

    bool isPlaying = false;
    List<CutsceneLine> currentLines;
    int currentIndex = 0;
    int requiredMission = 0;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void StartCutscene(List<CutsceneLine> lines, int missionOrderRequired)
{
    if (isPlaying) return; // Cegah overlap

    if (MissionManager.Instance.currentMission != missionOrderRequired)
        return;

    currentLines = lines;
    currentIndex = 0;
    requiredMission = missionOrderRequired;

    isPlaying = true;

    dialoguePanel.SetActive(true);
    StopAllCoroutines();
    StartCoroutine(PlayCutscene());
}

    IEnumerator PlayCutscene()
    {
        while (currentIndex < currentLines.Count)
        {
            CutsceneLine line = currentLines[currentIndex];

            yield return StartCoroutine(TypeText(line.text));

            yield return new WaitForSeconds(line.duration);

            currentIndex++;
        }

        EndCutscene();
    }

    IEnumerator TypeText(string text)
    {
        dialogueText.text = "";
        
        foreach (char c in text)
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(typeSpeed);
        }
    }

    void EndCutscene()
    {
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
        isPlaying = false;

        // Validasi mission agar naik
        MissionManager.Instance.ValidateMission(requiredMission);

        Debug.Log("Cutscene selesai — Mission updated.");
    }
}

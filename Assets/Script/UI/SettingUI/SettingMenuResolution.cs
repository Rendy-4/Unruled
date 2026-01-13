using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;



public class SettingMenuResolution : MonoBehaviour, IDataPresistence
{
    public TMP_Dropdown ResDropdown;
    public Toggle FullscreenTogle;

    Resolution[] AllResolution;
    List<Resolution> SelectedResolutionList = new List<Resolution>();

    int SelectedResolution = 0;
    bool IsFullScreen = true;

    bool loadingFromSave = false;

    int pendingResolutionIndex = -1;
    bool pendingFullscreen;

    void Awake()
    {
        AllResolution = Screen.resolutions;

        ResDropdown.ClearOptions();

        List<string> resolutionStringList = new List<string>();

        foreach (Resolution res in AllResolution)
        {
            string newRes = res.width + " x " + res.height;

            if (!resolutionStringList.Contains(newRes))
            {
                resolutionStringList.Add(newRes);
                SelectedResolutionList.Add(res);
            }
        }

        ResDropdown.AddOptions(resolutionStringList);

    }

    void Start() {
        // UI event listener
        ResDropdown.onValueChanged.AddListener(delegate { OnResolutionChanged(); });
        FullscreenTogle.onValueChanged.AddListener(delegate { OnFullscreenChanged(); });

        if (pendingResolutionIndex != -1 && SelectedResolutionList.Count > 0)
        {
            loadingFromSave = true;

            SelectedResolution = Mathf.Clamp(pendingResolutionIndex, 0, SelectedResolutionList.Count -1);
            IsFullScreen = pendingFullscreen;

            ResDropdown.value = SelectedResolution;
            FullscreenTogle.isOn = IsFullScreen;

            ApplyResolution();

            loadingFromSave = false;
        }
    }

    // ======================================
    // EVENT UI
    // ======================================
    public void OnResolutionChanged()
    {
        if (loadingFromSave) return; // cegah overwrite saat Load

        SelectedResolution = ResDropdown.value;
        ApplyResolution();

        DataPresistenceManager.instance?.SaveGame();
    }

    public void OnFullscreenChanged()
    {
        if (loadingFromSave) return;

        IsFullScreen = FullscreenTogle.isOn;
        ApplyResolution();

        DataPresistenceManager.instance?.SaveGame();
    }

    // ======================================
    // APPLY
    // ======================================
    void ApplyResolution()
    {
        if (SelectedResolutionList == null || SelectedResolutionList.Count == 0)
        {
            Debug.LogWarning("No available resolutions to apply!");
            return;
        }
        if (SelectedResolution < 0 || SelectedResolution >= SelectedResolutionList.Count)
        {
            Debug.LogWarning("Selected resolution index is out of range! Reset to 0");
            SelectedResolution = 0;
            ResDropdown.value = 0;
        }

        Screen.SetResolution(
            SelectedResolutionList[SelectedResolution].width,
            SelectedResolutionList[SelectedResolution].height,
            IsFullScreen);
    }

    void OnDisable()
    {
        if (!loadingFromSave)
        {
            DataPresistenceManager.instance?.SaveGame();
        }
    }

    public void LoadData(GameData data)
    {
       pendingResolutionIndex = data.resolutionIndex;
       pendingFullscreen = data.isFullscreen;
    }

    public void SaveData(ref GameData data)
    {
        data.resolutionIndex = SelectedResolution;
        data.isFullscreen = IsFullScreen;
    }
}

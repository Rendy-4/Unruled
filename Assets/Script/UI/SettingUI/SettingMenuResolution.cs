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

    void Start()
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

        // UI event listener
        ResDropdown.onValueChanged.AddListener(delegate { OnResolutionChanged(); });
        FullscreenTogle.onValueChanged.AddListener(delegate { OnFullscreenChanged(); });
    }

    // ======================================
    // EVENT UI
    // ======================================
    void OnResolutionChanged()
    {
        if (loadingFromSave) return; // cegah overwrite saat Load

        SelectedResolution = ResDropdown.value;
        ApplyResolution();
    }

    void OnFullscreenChanged()
    {
        if (loadingFromSave) return;

        IsFullScreen = FullscreenTogle.isOn;
        ApplyResolution();
    }

    // ======================================
    // APPLY
    // ======================================
    void ApplyResolution()
    {
        Screen.SetResolution(
            SelectedResolutionList[SelectedResolution].width,
            SelectedResolutionList[SelectedResolution].height,
            IsFullScreen);
    }

    public void LoadData(GameData data)
    {
        loadingFromSave = true; //supaya tidak overwrite

        SelectedResolution = data.resolutionIndex;
        IsFullScreen = data.isFullscreen;

        ResDropdown.value = SelectedResolution;
        FullscreenTogle.isOn = IsFullScreen;

        ApplyResolution();

        loadingFromSave = false; // aktifkan event lagi
    }

    public void SaveData(ref GameData data)
    {
        data.resolutionIndex = SelectedResolution;
        data.isFullscreen = IsFullScreen;
    }
}

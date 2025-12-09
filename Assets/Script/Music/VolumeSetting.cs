using UnityEngine;
using UnityEngine.UI;

public class VolumeSetting : MonoBehaviour, IDataPresistence
{
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    private float musicVolume = 1f;
    private float sfxVolume = 1f;

    private bool isLoading = false;

    private void Start()
    {
        musicSlider.onValueChanged.AddListener(SetMusic);
        sfxSlider.onValueChanged.AddListener(SetSFX);

        // Apply first time (if load belum jalan)
        ApplyMusic(musicVolume);
        ApplySFX(sfxVolume);
    }

    public void SetMusic(float value)
    {
        if (isLoading) return;
        musicVolume = value;
        ApplyMusic(value);
    }

    public void SetSFX(float value)
    {
        if (isLoading) return;
        sfxVolume = value;
        ApplySFX(value);
    }

    private void ApplyMusic(float value)
    {
        AudioManager.Instance.SetMusicVolume(value);
    }

    private void ApplySFX(float value)
    {
        AudioManager.Instance.SetSFXVolume(value);
    }

    // ---------- SAVE LOAD ----------
    public void LoadData(GameData data)
    {
        isLoading = true;

        musicVolume = data.musicVolume;
        sfxVolume = data.sfxVolume;

        musicSlider.value = musicVolume;
        sfxSlider.value = sfxVolume;

        ApplyMusic(musicVolume);
        ApplySFX(sfxVolume);

        isLoading = false;
    }

    public void SaveData(ref GameData data)
    {
        data.musicVolume = musicVolume;
        data.sfxVolume = sfxVolume;
    }
}

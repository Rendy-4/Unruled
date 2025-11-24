using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeSetting : MonoBehaviour, IDataPresistence
{
    [SerializeField] private AudioMixer MyMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider SFXSlider;

    float musicVolume = 1f;
    float sfxVolume = 1f;

    private void Start()
    {
        // Saat pertama running, slider HARUS memanggil event perubahan
        ApplyMusicVolume(musicVolume);
        ApplySFXVolume(sfxVolume);

       // slider mengupdate variabel
        musicSlider.onValueChanged.AddListener(OnMusicSliderChanged);
        SFXSlider.onValueChanged.AddListener(OnSFXSliderChanged);
    }

    public void OnMusicSliderChanged(float value)
    {
        musicVolume = value;
        ApplyMusicVolume(value);
    }

    public void OnSFXSliderChanged(float value)
    {
        sfxVolume = value;
        ApplySFXVolume(value);
    }

    void ApplyMusicVolume(float value)
    {
        MyMixer.SetFloat("Music", Mathf.Log10(value) * 20);
    }

    void ApplySFXVolume(float value)
    {
        MyMixer.SetFloat("SFX", Mathf.Log10(value) * 20);
    }

    public void LoadData(GameData data)
    {
        musicVolume = data.musicVolume;
        sfxVolume = data.sfxVolume;

        musicSlider.value = musicVolume;
        SFXSlider.value = sfxVolume;

        ApplyMusicVolume(musicVolume);
        ApplySFXVolume(sfxVolume);
    }

    public void SaveData(ref GameData data)
    {
        data.musicVolume = musicVolume;
        data.sfxVolume = sfxVolume;
    }
}

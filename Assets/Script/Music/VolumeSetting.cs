using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeSetting : MonoBehaviour, IDataPresistence
{
    [SerializeField] private AudioMixer MyMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider SFXSlider;

    private float musicVolume = 1f;
    private float sfxVolume = 1f;
    private bool loaded = false;

    private void Start()
    {
        // Listener dipasang sekali
        musicSlider.onValueChanged.AddListener(SetMusic);
        SFXSlider.onValueChanged.AddListener(SetSFX);

        //(default / load)
        ApplyMusic(musicVolume);
        ApplySFX(sfxVolume);
    }

    void SetMusic(float value)
    {
        if (!loaded) return;
        musicVolume = value;
        ApplyMusic(value);
    }

    void SetSFX(float value)
    {
        if (!loaded) return;
        sfxVolume = value;
        ApplySFX(value);
    }

    // Apply ke Mixer
    private void ApplyMusic(float value)
    {
        float dB = (value <= 0.0001f) ? -80f : Mathf.Log10(value) * 20f;
        MyMixer.SetFloat("Music", dB);
    }

    private void ApplySFX(float value)
    {
        float dB = (value <= 0.0001f) ? -80f : Mathf.Log10(value) * 20f;
        MyMixer.SetFloat("SFX", dB);
    }

    
    public void LoadData(GameData data)
    {
        loaded = false;

        musicVolume = data.musicVolume;
        sfxVolume = data.sfxVolume;

        musicSlider.value = musicVolume;
        SFXSlider.value = sfxVolume;

        ApplyMusic(musicVolume);
        ApplySFX(sfxVolume);

        loaded = true;
    }

   
    public void SaveData(ref GameData data)
    {
        data.musicVolume = musicVolume;
        data.sfxVolume = sfxVolume;
    }
}

using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    [Header("Audio Source")]
    [SerializeField] AudioSource BGMSource;
    [SerializeField] AudioSource SFXSource;

    [Header("Clips")]
    public AudioClip Background;
    public AudioClip ButtonClick;
    public AudioClip WalkOnGrass;
    public AudioClip WalkOnStone;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
       
    }

    private void Start()
    {
        if (Background != null)
        {
            BGMSource.clip = Background;
            BGMSource.loop = true;
            BGMSource.Play();
        }
    }

    public void SetMusicVolume(float value)
    {
        BGMSource.volume = value;
    }

    public void SetSFXVolume(float value)
    {
        SFXSource.volume = value;
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}

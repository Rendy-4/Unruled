using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    [Header("-------------------Audio Source-------------------")]
    [SerializeField] AudioSource BGMSource;
    [SerializeField] AudioSource SFXSource;

    [Header("-------------------Audio Clip-------------------")]
    public AudioClip Background;
    [Header("SFX")]
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
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        BGMSource.clip = Background;
        BGMSource.loop = true;
        BGMSource.volume = 0.8f;
        SFXSource.volume = 0.8f;
        BGMSource.Play();
    }
    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}

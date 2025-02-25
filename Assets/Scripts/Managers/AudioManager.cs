using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] AudioSource seSource;

    [Header("Audio Options Sliders")]
    [SerializeField] AudioMixer masterAudioMixer;
    [SerializeField] Slider masterSlider;
    [SerializeField] Slider soundEffectSlider;
    [SerializeField] Slider musicSlider;

    [Header("Singleton")]
    public bool dontDestroyOnLoad = true;
    public static AudioManager Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);

        if (dontDestroyOnLoad)
            DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        InitializeSliders();
    }

    void InitializeSliders()
    {
        masterSlider.minValue = 0.001f;
        masterSlider.maxValue = 1.000f;

        soundEffectSlider.minValue = 0.001f;
        soundEffectSlider.maxValue = 1.000f;

        masterSlider.minValue = 0.001f;
        masterSlider.maxValue = 1.000f;
    }

    public void ChangeMixerVolume(string mixer)
    {
        switch (mixer.ToLower())
        {
            case "master":
                masterAudioMixer.SetFloat("Master", Mathf.Log10(masterSlider.value) * 20f);
                Debug.Log("Changed master volume");
                break;
            case "soundeffects":
                masterAudioMixer.SetFloat("SoundEffects", Mathf.Log10(soundEffectSlider.value) * 20f);
                Debug.Log("Changed sound volume");
                break;
            case "music":
                Debug.Log("Changed music volume");
                masterAudioMixer.SetFloat("Music", Mathf.Log10(musicSlider.value) * 20f);
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Plays given SoundEffect from AudioManagers AudioSource
    /// </summary>
    /// <param name="seToPlay"></param>
    public void PlaySoundEffect(SoundEffect seToPlay)
    {
        seSource.spatialBlend = 0;
        seSource.pitch = seToPlay.pitch;
        seSource.outputAudioMixerGroup = seToPlay.audioGroup;

        AudioClip clip = seToPlay.GetClip();

        if (clip != null)
            seSource.PlayOneShot(clip, seToPlay.volume);
    }

    /// <summary>
    /// Plays SoundEffect from the given object. If object does not have AudioSource
    /// This adds an AudioSource Component to the object
    /// </summary>
    /// <param name="seToPlay"></param>
    /// <param name="objToPlayFrom"></param>
    public void PlaySoundEffect(SoundEffect seToPlay, GameObject objToPlayFrom)
    {
        AudioSource objSource = objToPlayFrom.GetComponent<AudioSource>();

        // If given object does now have an AudioSource, we add one to it
        if (objSource == null)
            objSource = objToPlayFrom.AddComponent<AudioSource>();

        objSource.volume = seToPlay.volume;
        objSource.pitch = seToPlay.pitch;
        objSource.spatialBlend = seToPlay.spatialBlend;
        objSource.outputAudioMixerGroup = seToPlay.audioGroup;

        AudioClip clip = seToPlay.GetClip();
        objSource.clip = clip;

        if (clip != null)
            objSource.Play();
    }
}

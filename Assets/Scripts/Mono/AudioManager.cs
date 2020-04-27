using System;
using UnityEngine;

[System.Serializable()]
public struct SoundParameters
{
    [Range(0, 1)]
    public float Volume;
    [Range(-3, 3)]
    public float Pitch;
    public bool Loop;
    [Range(0f, 1f)]
    public float spatial3DAudioBlend;
   
}
[System.Serializable()]
public class SoundAsset
{
    #region Variables

    [SerializeField] private String name = String.Empty;
    public String Name { get { return name; } }

    [SerializeField] private AudioClip clip = null;
    public AudioClip Clip { get { return clip; } }

    [SerializeField] private SoundParameters parameters = new SoundParameters();
    public SoundParameters Parameters { get { return parameters; } }

    [HideInInspector]
    public AudioSource Source = null;

    #endregion

    public void PlayAudio()
    {
        Source.clip = Clip;

        Source.volume = Parameters.Volume;
        Source.pitch = Parameters.Pitch;
        Source.loop = Parameters.Loop;
        Source.spatialBlend = Parameters.spatial3DAudioBlend;

        Source.Play();
    }
    public void StopAudio()
    {
        Source.Stop();
    }

    public void PauseAudio()
    {
        Source.Pause();
    }
}
public class AudioManager : MonoBehaviour {

    #region Variables

    public static       AudioManager    Instance        = null;

    [SerializeField] private SoundAsset[]         sounds          = null;
    [SerializeField] private AudioSource     sourcePrefab    = null;

    //[SerializeField]    String          startupTrack    = String.Empty;

    #endregion

    #region Default Unity methods

    /// <summary>
    /// Function that is called on the frame when a script is enabled just before any of the Update methods are called the first time.
    /// </summary>
    private void Awake()
    {
        if (Instance != null)
        { Destroy(gameObject); }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        
    }
    /// <summary>
    /// Function that is called when the script instance is being loaded.
    /// </summary>
    private void Start()
    {
       InitSounds();
       PlaySound("Theme");
        
    }

    #endregion

    /// <summary>
    /// Function that is called to initializes sounds.
    /// </summary>
    private void InitSounds()
    {
        foreach (var sound in sounds)
        {
            var source = Instantiate(sourcePrefab, gameObject.transform);
            source.name = sound.Name;

            sound.Source = source;
        }
    }

    /// <summary>
    /// Function that is called to play a sound.
    /// </summary>
    public void PlaySound(string name)
    {
        var sound = GetSound(name);
        if (sound != null)
        {
            sound.PlayAudio();
        }
        else
        {
            Debug.LogWarning("Sound by the name " + name + " is not found! Issues occured at AudioManager.PlaySound()");
        }
    }

    // Function called when you want to pause a playing sound
    public void PauseSound(string name)
    {
        var sound = GetSound(name);
        if (sound != null)
        {
            sound.PauseAudio();
        }
        else
        {
            Debug.LogWarning("Sound by the name " + name + " is not found! Issues occured at AudioManager.PauseSound()");
        }

    }
    /// <summary>
    /// Function that is called to stop a playing sound.
    /// </summary>
    public void StopSound(string name)
    {
        var sound = GetSound(name);
        if (sound != null)
        {
            sound.StopAudio();
        }
        else
        {
            Debug.LogWarning("Sound by the name " + name + " is not found! Issues occured at AudioManager.StopSound()");
        }
    }

    #region Getters

    private SoundAsset GetSound(string name)
    {
        foreach (var sound in sounds)
        {
            if (sound.Name == name)
            {
                return sound;
            }
        }
        return null;
    }

    #endregion
}
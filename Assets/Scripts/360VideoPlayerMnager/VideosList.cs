using UnityEngine.Video;
using UnityEngine;


[System.Serializable]

public class VideosList : MonoBehaviour
{
    public string Videoname;

    public VideoClip videoClip;

    [Range(0f, 1f)]
    public float volume = .75f;

    public bool loop = false;

    public bool playOnAwake = false;

    [Range(0f, 1f)]
    public float playBackSpeed = 1f;

    [HideInInspector]
    public VideoPlayer source;
}

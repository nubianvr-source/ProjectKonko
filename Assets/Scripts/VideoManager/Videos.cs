using UnityEngine.Video;
using UnityEngine;

[System.Serializable]
public class Videos{
    public string videoName;

    public VideoClip videoClip;

    public bool loop = false;

    public bool playOnWake = false;

   [HideInInspector] public VideoPlayer videoPlayer;
}

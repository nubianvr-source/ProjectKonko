using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Konko.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoSplashScreen : MonoBehaviour
{
    public SplashClip[] clips;

    public RawImage display;

    // VideoPlayer component attached to this gameObject
    VideoPlayer videoPlayer;

    AudioSource audioPlayer;

    SceneManager sceneManager;

    int currentIndex, targetIndex;

    // Start is called before the first frame update
    void Awake()
    {
        sceneManager = FindObjectOfType<SceneManager>();

        videoPlayer = GetComponent<VideoPlayer>();
        audioPlayer = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void OnEnable()
    {
        Next();

        videoPlayer.loopPointReached += player =>
        {
            Next();
        };
    }

    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
        {
            if (!clips[currentIndex].noSkip) Next();
        }
    }

    void Next()
    {
        StopCoroutine(playVideo());

        if (targetIndex >= clips.Length)
        {
            sceneManager.LoadScene("main_menu");
        }
        else
        {
            currentIndex = targetIndex;
            targetIndex++;
            videoPlayer.clip = clips[currentIndex].clip;
            StartCoroutine(playVideo());
        }
    }



    IEnumerator playVideo()
    {
        videoPlayer.Prepare();
        while (!videoPlayer.isPrepared)
        {
            yield return null;
        }

        display.texture = videoPlayer.texture;
        videoPlayer.Play();
        audioPlayer.Play();

    }








    [Serializable]
    public class SplashClip
    {
        public VideoClip clip;
        public bool noSkip;
    }
}

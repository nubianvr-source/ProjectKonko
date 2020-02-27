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

    bool skipped;

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
        print(currentIndex);
        print(targetIndex);
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
            if (!clips[currentIndex].noSkip && !skipped)
            {
                Next();
                skipped = true;
            }
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

        skipped = false;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VideoSplashScreen : MonoBehaviour
{
    public SplashClip[] splashClips;

    // VideoPlayer component attached to this gameObject
    VideoPlayer videoPlayer;

    int currentClip = 0;

    // Start is called before the first frame update
    void Awake()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.clip = splashClips[currentClip].clip;
        videoPlayer.Play();
    }

    // Start is called before the first frame update
    void OnEnable()
    {
        videoPlayer.loopPointReached += OnVideoEnd;
    }

    void Update()
    {
        if ((Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2)) && !splashClips[currentClip].noSkip)
        {
            NextClip();
        }
    }


    public void NextClip()
    {
        currentClip++;

        if (currentClip >= splashClips.Length)
        {
            LoadActualGame();
            return;
        }

        videoPlayer.clip = splashClips[currentClip].clip;
        videoPlayer.Play();
    }

    // LoadActualGame loads the actual game
    void LoadActualGame()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

    void OnVideoEnd(VideoPlayer vPlayer)
    {
        NextClip();
    }




    [System.Serializable]
    public class SplashClip
    {
        public VideoClip clip;
        public bool noSkip;
    }
}

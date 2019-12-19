using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VideoSplashScreen : MonoBehaviour
{
    // VideoPlayer component attached to this gameObject
    VideoPlayer videoPlayer;

    // Start is called before the first frame update
    void Awake()
    {
        videoPlayer = GetComponent<VideoPlayer>();
    }

    // Start is called before the first frame update
    void OnEnable()
    {
        videoPlayer.loopPointReached += LoadActualGame;
    }

    // LoadActualGame loads the actual game
    void LoadActualGame(VideoPlayer vPlayer)
    {
        SceneManager.LoadScene(1);
    }
}

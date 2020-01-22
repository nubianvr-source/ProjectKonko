using System;
using UnityEngine.Video;
using UnityEngine;

public class VideoPlayerCylinder : MonoBehaviour
{
    public static VideoPlayerCylinder instance;

    public Videos[] videoList;

    void Awake()
    {
       /* 
         if (instance != null)
        {
            Destroy(gameObject);
        }
        else 
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        */

        foreach (Videos v in videoList)
        {
            v.videoPlayer = gameObject.GetComponent<VideoPlayer>();
            v.videoPlayer.clip = v.videoClip;
            v.videoPlayer.isLooping = v.loop;
        }
        
    }

    public void PlayVideo(string video)
    {
        Videos v = Array.Find(videoList, item => item.videoName == video);
        if (v == null)
        {
            Debug.LogError("Video:" + video + "not found!");
            return;
        }
        v.videoPlayer.Prepare();
        v.videoPlayer.Play();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

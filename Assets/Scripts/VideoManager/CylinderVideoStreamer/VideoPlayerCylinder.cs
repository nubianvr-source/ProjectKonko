using System;
using UnityEngine;
using UnityEngine.Video;

namespace VideoManager.CylinderVideoStreamer
{
    public class VideoPlayerCylinder : MonoBehaviour
    {


        public Videos[] videoList;
        //public VideoPlayer videoPlayerRef ;

        private void Awake()
        {
            foreach (var video in videoList)
            {
                video.videoPlayer = gameObject.GetComponent<VideoPlayer>();
                video.videoPlayer.name = video.videoName;
                video.videoPlayer.clip = video.videoClip;
                video.videoPlayer.isLooping = video.loop;
            }
        
        }

        public void PlayVideo(string videoName)
        {
            var videos = Array.Find(videoList, video => video.videoName == videoName);
            videos.videoPlayer.Play();
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
}

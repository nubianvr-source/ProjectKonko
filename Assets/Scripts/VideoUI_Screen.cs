using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityEngine.Video;

namespace NubianVR.UI
{
    public class VideoUI_Screen : UI_Screen
    {
        #region Variables
        public VideoPlayer videoPlayer;
        [FormerlySerializedAs("splashVideoDisplay")] public RawImage centerVideoDisplay;
        public RawImage leftVideoDisplay;
        public RawImage rightVideoDisplay;
        public GameObject leftVideoPanel;
        public GameObject rightVideoPanel;
        public float delay = 0.3f;
        public Sprite playIcon;
        public Sprite pauseIcon;
        public VideoClip[] lessonVideos;
        public UnityEvent onLessonVideosFinished = new UnityEvent();
        
        private VideoPlayer _videoPlayerRef;
        private readonly Color32 _rawImageHoverExit = new Color32(255, 255, 255, 255);
        private readonly Color32 _rawImageHoverEnter = new Color32(128, 128, 128, 255);
        [SerializeField] private int currentIndex;
        private int _targetIndex;
        private int _currentIndex;
        private int _nextIndex;
        private bool _videoJustWatched = false;
        
        #endregion

        #region MainMethods

        void Update()
        {
            // videoPlayer.loopPointReached += player => { PlayNextVideo();};
        }

        #endregion
        #region HelperMethods

        public override void StartScreen()
        {
            base.StartScreen();
            videoPlayer.loopPointReached += player =>
            {
                _videoJustWatched = false;
                PlayNextVideo();
            };
        }

        public void PlayNextVideo()
        {
            StopCoroutine(playVideo());

            if (_videoJustWatched)
            {
                print("Next Screen");
                onLessonVideosFinished.Invoke();
            }
            else
            {
                if (_nextIndex >= lessonVideos.Length)
                { 
                    _currentIndex = _nextIndex;
                    _nextIndex++;
                    videoPlayer.clip = lessonVideos[_currentIndex];
                    StartCoroutine(playVideo());
                    
                }
                else
                {
                    _nextIndex = 0;
                }

               
            }
        }
        
        IEnumerator playVideo()
        {
            videoPlayer.Prepare();
            while (!videoPlayer.isPrepared)
            {
                yield return null;
            }

            centerVideoDisplay.texture = videoPlayer.texture;
            videoPlayer.Play();
        }
        
        #endregion
    }
}
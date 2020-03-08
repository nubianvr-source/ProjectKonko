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
        [FormerlySerializedAs("splashVideos")] public VideoClip[] lessonVideos;
        [FormerlySerializedAs("onSplashVideosFinished")] public UnityEvent onLessonVideoFinished = new UnityEvent();


        private int _currentIndex = 0;
        private int _nextIndex = 0;
        #endregion

        #region MainMethods
        

        #endregion
        #region HelperMethods

        public override void StartScreen()
        {
            base.StartScreen();

            videoPlayer.loopPointReached += player =>
            {
               PlayNextVideo();
            };
        }

        public void PlayNextVideo()
        {
            StopVideo();
            if (_nextIndex >= lessonVideos.Length)
            {
                print("Next Screen");
                _nextIndex = 0;
                onLessonVideoFinished.Invoke();
            }
            else
            {
                _currentIndex = _nextIndex;
                _nextIndex++;
                videoPlayer.clip = lessonVideos[_currentIndex];
                StartCoroutine(playVideo());
            }
        }

       

        public void StopVideo()
        {
            videoPlayer.Stop();
            StopCoroutine(playVideo());
            centerVideoDisplay.texture = null;
            leftVideoDisplay.texture = null;
            rightVideoDisplay.texture = null;
        }

        IEnumerator playVideo()
        {
            videoPlayer.Prepare();
            while (!videoPlayer.isPrepared)
            {
                yield return null;
            }

            var videoTexture = videoPlayer.texture;
            centerVideoDisplay.texture = videoTexture;
            leftVideoDisplay.texture = videoTexture;
            rightVideoDisplay.texture = videoTexture;
            videoPlayer.Play();
        }
        
        #endregion
    }
}
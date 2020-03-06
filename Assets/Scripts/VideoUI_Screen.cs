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
        public UI_Screen[] transitionScreens;
        public UI_System UIManager;
        
        
        private int _currentIndex = 0;
        private int _nextIndex = 0;
        #endregion

        #region MainMethods

        void Update()
        {
           
        }

        #endregion
        #region HelperMethods

        public override void StartScreen()
        {
            base.StartScreen();

            videoPlayer.loopPointReached += player =>
            {
                TransitionToScreen();
            };
        }

        public void PlayNextVideo()
        {
            print(lessonVideos.Length);
            //StopVideo();
            videoPlayer.clip = lessonVideos[_currentIndex];
            StartCoroutine(playVideo());
        }

        public void TransitionToScreen()
        {
            
            print("currentIndex value = " + _currentIndex);
            UIManager.SwitchScreens(transitionScreens[_currentIndex]);
            StopVideo();
           
        }

        public void StopVideo()
        {
            videoPlayer.Stop();
            StopCoroutine(playVideo());
            centerVideoDisplay.texture = null;
            leftVideoDisplay.texture = null;
            rightVideoDisplay.texture = null;
            
            if (_nextIndex <= lessonVideos.Length)
            {
                _currentIndex = _nextIndex;
                 _nextIndex++;
                 print("current index" + _currentIndex);
                 print("next index" + _nextIndex);
            }
            else
            {
               _nextIndex = 0;
                _currentIndex = _nextIndex;
                print("current index" + _currentIndex);
                print("next index" + _nextIndex);
            }
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
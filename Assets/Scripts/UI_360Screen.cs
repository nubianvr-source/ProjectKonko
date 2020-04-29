using System.Collections;
using System.Collections.Generic;
using NubianVR.UI;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.Events;

namespace NubianVR.UI
{
    public class UI_360Screen : UI_Screen

    {
        #region Variables

        public VideoPlayer videoPlayer;
        public VideoClip[] videoExperiences;
        public UnityEvent onVideoExperienceVideoFinished = new UnityEvent();

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
                        SoundManager.instance.Play("InGameTheme");
                        onVideoExperienceVideoFinished.Invoke();
                    };
                }

         public void PlayNextVideo()
         {
             StopVideo();
            SoundManager.instance.Stop("InGameTheme");
            videoPlayer.clip = videoExperiences[_currentIndex];
             StartCoroutine(PlayVideo());
         }

         public void StopVideo()
         {
             videoPlayer.Stop();
             StopCoroutine(PlayVideo());
             if (_nextIndex >= videoExperiences.Length)
             {
                 _nextIndex = 0;
                 _currentIndex = _nextIndex;
             }
             else
             {
                 _currentIndex = _nextIndex;
                 _nextIndex++;
             }
         }


         IEnumerator PlayVideo()
         {
             videoPlayer.Prepare();
             while (!videoPlayer.isPrepared)
             {
                 yield return null;
             }
             
             videoPlayer.Play();
         }
         
        #endregion
        
        
    }
}
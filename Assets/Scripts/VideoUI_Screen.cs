using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.EventSystems;

namespace NubianVR.UI
{
    public class VideoUI_Screen : UI_Screen//, IDragHandler, IPointerDownHandler
    {
        #region Variables
        public VideoPlayer videoPlayer;
        [FormerlySerializedAs("splashVideoDisplay")] public RawImage centerVideoDisplay;
        public RawImage leftVideoDisplay;
        public RawImage rightVideoDisplay;
        public Image videoProgressBar;
        [FormerlySerializedAs("splashVideos")] public VideoClip[] lessonVideos;
        [FormerlySerializedAs("onSplashVideosFinished")] public UnityEvent onLessonVideoFinished = new UnityEvent();
        

        private int _currentIndex = 0;
        private int _nextIndex = 0;
        #endregion

        #region MainMethods
        private void Awake()
        {
           
        }

      /*  private void Update()
        {
            if (videoPlayer.frameCount > 0)
            {
                videoProgressBar.fillAmount = (float)videoPlayer.frame / (float)videoPlayer.frameCount;
            }
        }
        */

        #endregion
        #region HelperMethods

        public override void StartScreen()
        {
            base.StartScreen();
            videoProgressBar = GetComponent<Image>();
            videoPlayer.loopPointReached += player =>
            {
               PlayNextVideo();
               
            };
        }

        public void PlayNextVideo()
        {
            StopVideo();
            SoundManager.instance.Play("InGameTheme");
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
                SoundManager.instance.Stop("InGameTheme");
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
        
       /* public void OnPointerDown(PointerEventData eventData)
        {
            TrySkip(eventData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            TrySkip(eventData);
        }

        private void TrySkip(PointerEventData eventData)
        {
            Vector2 localPoint;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
                videoProgressBar.rectTransform, eventData.position, null, out localPoint))
            {
                float pct = Mathf.InverseLerp(videoProgressBar.rectTransform.rect.xMin, videoProgressBar.rectTransform.rect.xMax, localPoint.x);
                SkipToPercent(pct);
            }
        }

        private void SkipToPercent(float pct)
        {
            var frame = videoPlayer.frameCount * pct;
            videoPlayer.frame = (long)frame;
        }
        */
        #endregion
    }
}
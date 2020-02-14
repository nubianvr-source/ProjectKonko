using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using System;
using UnityEngine.Serialization;

public class VideoManager2D : MonoBehaviour
{
    public RawImage rawImageAssetLeft;
    public RawImage rawImageAssetRight;
    public RawImage rawImageAssetCenter;
    public string nextView;
    public GameObject leftVideoPanel;
    public GameObject rightVideoPanel;
    public Konko.UIManagement.UIManager uiManager;
    public float delay = 0.3f;
    public Sprite playIcon;
    public Sprite pauseIcon;
    public Videos[] videoList;

    private VideoPlayer _videoPlayerRef;
    private readonly Color32 _rawImageHoverExit = new Color32(255, 255, 255, 255);
    private readonly Color32 _rawImageHoverEnter = new Color32(128, 128, 128, 255);
    [SerializeField] private int currentIndex;
    private int _targetIndex;
    
    
    private void Awake()
    {
        foreach (var video in videoList)
        {
            video.videoPlayer  = gameObject.AddComponent<VideoPlayer>();
            video.videoPlayer.name = video.videoName;
            video.videoPlayer.clip = video.videoClip;
            video.videoPlayer.isLooping = video.loop;
            video.videoPlayer.playOnAwake = video.playOnWake;
        }

        if (currentIndex < _targetIndex)
        {
            _videoPlayerRef = videoList[currentIndex].videoPlayer;
            videoList[currentIndex].playOnWake = true;
            PreparePlayPauseButton(videoList[currentIndex].videoName);
        }
        else
        {
            currentIndex = 0;

        }
    }


    private void Start()
    {
        _videoPlayerRef.loopPointReached += (player) => { NextUIView(); };
        _targetIndex = videoList.Length;
    }

    private IEnumerator PlayVideo(string videoName)
    {
        var videos = Array.Find(videoList, video => video.videoName == videoName);
        _videoPlayerRef = videos.videoPlayer;
        _videoPlayerRef.Prepare();
        var waitForSeconds = new WaitForSeconds(delay);
        while (!_videoPlayerRef.isPrepared) 
        {
            yield return waitForSeconds;
        }
        rawImageAssetCenter.color = new Color32(255, 255, 255, 255);
        var videoPlayerTexture = _videoPlayerRef.texture;
        rawImageAssetLeft.texture = videoPlayerTexture;
        rawImageAssetRight.texture = videoPlayerTexture;
        rawImageAssetCenter.texture = videoPlayerTexture;
        _videoPlayerRef.Play();
    }

    public void StartPlayingVideo(string videoName)
    {
        StartCoroutine(PlayVideo(videoName));
    }
    

    private void NextUIView()
    {
        rawImageAssetLeft.texture = null;
        rawImageAssetRight.texture = null;
        rawImageAssetCenter.texture = null;
        uiManager.ShowOnlyScreenFadeOn(nextView);
        leftVideoPanel.SetActive(false);
        rightVideoPanel.SetActive(false);
        AudioManager.Instance.PlaySound("Theme");
        currentIndex++;
    }

    public void OnHoverEnter()
    {
        if (_videoPlayerRef.isPlaying)
        {
            OnHoverEnterColorChange();
        }
    }

    public void OnHoverExit()
    {
        if (_videoPlayerRef.isPlaying)
        {
            OnHoverExitColorChange();
        }
        else
        {
            OnHoverEnterColorChange();
        }
    }

    private void OnHoverEnterColorChange()
    {
        GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        rawImageAssetCenter.GetComponent<RawImage>().color = _rawImageHoverEnter;
        rawImageAssetLeft.GetComponent<RawImage>().color = _rawImageHoverEnter;
        rawImageAssetRight.GetComponent<RawImage>().color = _rawImageHoverEnter;
    }

    private void OnHoverExitColorChange()
    {
        GetComponent<Image>().color = new Color32(255, 255, 255, 0);
        rawImageAssetCenter.GetComponent<RawImage>().color = _rawImageHoverExit;
        rawImageAssetLeft.GetComponent<RawImage>().color = _rawImageHoverExit;
        rawImageAssetRight.GetComponent<RawImage>().color = _rawImageHoverExit;

    }

    public void PreparePlayPauseButton(string videoName)
    {
        if (!_videoPlayerRef.isPlaying)
        {
            StartPlayingVideo(videoName);
            leftVideoPanel.SetActive(true);
            rightVideoPanel.SetActive(true);
            GetComponent<Image>().sprite = pauseIcon;
        }
        else
        {
            if (_videoPlayerRef.isPlaying)
            {
                _videoPlayerRef.Pause();
                GetComponent<Image>().sprite = playIcon;
            }
            else
            {
                _videoPlayerRef.Play();
                GetComponent<Image>().sprite = pauseIcon;

            }

        }
    }



}

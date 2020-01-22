﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using Konko.SceneManagement;

public class RawImageVideoStream : MonoBehaviour
{

    public VideoPlayer videoReference;
    public AudioSource audioReference;
    public RawImage rawImageAsset1;
    public RawImage rawImageAsset2;
    public RawImage rawImageAsset3;
    public GameObject CurrentView;
    public GameObject NextView;
    public GameObject LeftVideoPanel;
    public GameObject RightVideoPanel;
    SceneManager sceneManager;
    public string nextScene;
    public string currentScene;

    public float delay = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        sceneManager = FindObjectOfType<SceneManager>();
        videoReference.loopPointReached += (player) =>
        {
            nextUIView();
        };
    }

    void Update()
    {
        checkOver();
    }


    public void startPlayingVideoRefence()
    {
        StartCoroutine(playVideo());    
    
    }

    IEnumerator playVideo()
    {
        videoReference.Prepare();
        WaitForSeconds waitForSeconds = new WaitForSeconds(delay);
        while (!videoReference.isPrepared) 
        {
            yield return waitForSeconds;
        }
        rawImageAsset1.color = new Color32(255, 255, 255, 255);
        rawImageAsset1.texture = videoReference.texture;
        rawImageAsset2.texture = videoReference.texture;
        rawImageAsset3.texture = videoReference.texture;
        videoReference.Play();
        audioReference.Play();
    
    }

    public void nextUIView() 
    {
        videoReference.Stop();
        CurrentView.SetActive(false);
        NextView.SetActive(true);
        LeftVideoPanel.SetActive(false);
        RightVideoPanel.SetActive(false);
        sceneManager.UnloadScene("lessons");
    }

    private void checkOver()
    {
        long playerCurrentFrame = videoReference.GetComponent<VideoPlayer>().frame;
        long playerFrameCount = Convert.ToInt64(videoReference.GetComponent<VideoPlayer>().frameCount);

        if (playerCurrentFrame < playerFrameCount)
        {
          //  print("VIDEO IS PLAYING");
        }
        else
        {
            print("done playing");
            nextUIView();
        }
    }
}

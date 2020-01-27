using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;



public class RawImageVideoStream : MonoBehaviour
{

    public VideoPlayer videoReference;
    public AudioSource audioReference;
    public RawImage rawImageAsset1;
    public RawImage rawImageAsset2;
    public RawImage rawImageAsset3;
    public string NextView;
    public GameObject LeftVideoPanel;
    public GameObject RightVideoPanel;
    public Konko.UIManagement.UIManager uiManager;

    public float delay = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
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
        uiManager.ShowOnlyScreenFadeOn(NextView);
        LeftVideoPanel.SetActive(false);
        RightVideoPanel.SetActive(false);
        AudioManager.Instance.PlaySound("Theme");
        
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

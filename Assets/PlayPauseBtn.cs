using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class PlayPauseBtn : MonoBehaviour
{

    public GameObject LeftVideoPanel;
    public GameObject RightVideoPanel;
    public VideoPlayer videoPlayer;
    RawImageVideoStream CylinderVideo2d;
    public RawImage rawImageRight;
    public RawImage rawImageLeft;
    public RawImage rawImageCenter;
    public Sprite playIcon;
    public Sprite pauseIcon;

    
    Color32 rawImageHoverExit = new Color32(255, 255, 255, 255);
    Color32 rawImageHoverEnter = new Color32(128, 128, 128, 255);


    public void onHoverEnter()
    {
        if (videoPlayer.isPlaying)
        {
            onHoverEnterColorChange();
        }
    }

    public void onHoverExit()
    {
        if (videoPlayer.isPlaying)
        {
            onHoverExitColorChange();

        }
        else
        {
            onHoverEnterColorChange();
        }
    
    }

    public void onHoverEnterColorChange()
    {
        GetComponent<Image>().color = new Color32(255, 255, 255, 255);
       rawImageCenter.GetComponent<RawImage>().color = rawImageHoverEnter;
       rawImageLeft.GetComponent<RawImage>().color = rawImageHoverEnter;
       rawImageRight.GetComponent<RawImage>().color = rawImageHoverEnter;
    }

    public void onHoverExitColorChange()
    {
        GetComponent<Image>().color = new Color32(255, 255, 255, 0);
        rawImageCenter.GetComponent<RawImage>().color = rawImageHoverExit;
        rawImageLeft.GetComponent<RawImage>().color = rawImageHoverExit;
        rawImageRight.GetComponent<RawImage>().color = rawImageHoverExit;

    }

   

    public void preparePlayPauseVideo()
    {
        if (!videoPlayer.isPrepared)
        {
           
            CylinderVideo2d.startPlayingVideoRefence();
            LeftVideoPanel.SetActive(true);
            RightVideoPanel.SetActive(true);
            GetComponent<Image>().sprite = pauseIcon;
        }
        else 
        {
            if (videoPlayer.isPlaying)
            {
                videoPlayer.Pause();
                GetComponent<Image>().sprite = playIcon;
            }
            else
            {
                videoPlayer.Play();
                GetComponent<Image>().sprite = pauseIcon;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        CylinderVideo2d = FindObjectOfType<RawImageVideoStream>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

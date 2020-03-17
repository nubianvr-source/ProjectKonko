using System.Collections;
using System.Collections.Generic;
using NubianVR.UI;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using TMPro;
using VideoManager.CylinderVideoStreamer;

public class PlayPauseBtn : MonoBehaviour
{

    public GameObject LeftVideoPanel;
    public GameObject RightVideoPanel;
    public VideoPlayer videoPlayer;
    public VideoUI_Screen CylinderVideo2d;
    public RawImage rawImageRight;
    public RawImage rawImageLeft;
    public RawImage rawImageCenter;
    public Sprite playIcon;
    public Sprite pauseIcon;
    public Image playPauseIcon;
    public TextMeshProUGUI stateText;

    
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
        playPauseIcon.color = new Color32(255, 255, 255, 255);
        stateText.color = new Color32(255, 255, 255, 255);
        rawImageCenter.GetComponent<RawImage>().color = rawImageHoverEnter;
        rawImageLeft.GetComponent<RawImage>().color = rawImageHoverEnter;
        rawImageRight.GetComponent<RawImage>().color = rawImageHoverEnter;
    }

    public void onHoverExitColorChange()
    {
        playPauseIcon.color = new Color32(255, 255, 255, 0);
        stateText.color = new Color32(255, 255, 255, 0);
        rawImageCenter.GetComponent<RawImage>().color = rawImageHoverExit;
        rawImageLeft.GetComponent<RawImage>().color = rawImageHoverExit;
        rawImageRight.GetComponent<RawImage>().color = rawImageHoverExit;

    }

   

    public void preparePlayPauseVideo()
    {
        if (!videoPlayer.isPrepared)
        {
            playPauseIcon.sprite = pauseIcon;
            stateText.text = "Click to Pause";
        }
        else 
        {
            if (videoPlayer.isPlaying)
            {
                videoPlayer.Pause();
                playPauseIcon.sprite = playIcon;
                stateText.text = "Click to Play";
                
            }
            else
            {
                videoPlayer.Play();
                playPauseIcon.sprite = pauseIcon;
                stateText.text = "Click to Pause";
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
      //  CylinderVideo2d = FindObjectOfType<RawImageVideoStream>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

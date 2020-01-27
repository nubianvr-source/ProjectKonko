using System.Collections;
using System.Collections.Generic;
using UnityEngine.Video;
using UnityEngine;

public class VideoHnadler360 : MonoBehaviour
{

    public VideoPlayer videoPlayer;
    public GameObject nextUI;
    public Konko.UIManagement.UIManager uiManager;
    public Material videoSkyboxMat;
    public Material inGameSkyboxMat;
    public Skybox skybox;
    public GameObject uipointer;
    public GameObject vrgrabber;
    public string NextUIView;



    // Start is called before the first frame update
    void Start()
    {
        videoPlayer.loopPointReached += (player) =>
        {
            nextUIView();
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play360Video() 
    {
        videoPlayer.Play();
        skybox.material = videoSkyboxMat;
        uipointer.SetActive(false);
    
    }

    public void nextUIView()
    {
        videoPlayer.Stop();
        uiManager.ShowOnlyScreenFadeOn(NextUIView);
        // RenderSettings.skybox = inGameSkyboxMat;
        skybox.material = inGameSkyboxMat;
        vrgrabber.SetActive(true);
    }
}

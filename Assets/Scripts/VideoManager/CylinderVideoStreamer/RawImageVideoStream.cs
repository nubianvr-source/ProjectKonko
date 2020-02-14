using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace VideoManager.CylinderVideoStreamer
{
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
        public VideoClip[] videoClips;
        public PlayPauseBtn playPauseScript;
        public DialogueTrigger dialogueTriggerScript;
        public int videoClipIndex;

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
       
        }

        void Awake()
        {
            playPauseScript.preparePlayPauseVideo();
        }


        public void startPlayingVideoRefence()
        {
            // playNextVideo(videoClipIndex);
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
            rawImageAsset1.texture = null;
            rawImageAsset2.texture = null;
            rawImageAsset3.texture = null;
            uiManager.ShowOnlyScreenFadeOn(NextView);
            dialogueTriggerScript.TriggerDialogue();
            LeftVideoPanel.SetActive(false);
            RightVideoPanel.SetActive(false);
            AudioManager.Instance.PlaySound("Theme");
            
        }

      

    
    }
}

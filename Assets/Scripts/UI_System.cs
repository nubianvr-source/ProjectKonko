using System.Collections;
using System.Collections.Generic;
using System.Security.Permissions;
using Oculus.Platform;
using UnityEngine;
using UnityEngine.Assertions.Comparers;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace NubianVR.UI
{


    public class UI_System : MonoBehaviour
    {
        [Header("Main Properties")] 
        public UI_Screen m_StartScreen;
        
        [Header("System Events")] public UnityEvent onSwitchScreen = new UnityEvent();

        [Header("Fade Reference")] 
        public OVRScreenFade ScreenFade;

        [Header("OVRCameraRig")] public OVRCameraRig vrRig;



        #region Variables

        private Component[] screens = new Component[0];

        private UI_Screen _currentScreen;
        public UI_Screen currentScreen => _currentScreen;

        private UI_Screen _previousScreen;
        public UI_Screen previousScreen => _previousScreen;

        [FormerlySerializedAs("sceneNames")] public string[] scenes; 
            
        
        #endregion

        #region MainMethods

        // Start is called before the first frame update
        void Start()
        {
            FadeIn();
            screens = GetComponentsInChildren<UI_Screen>(true);
            InitializeScreens();
            if (!m_StartScreen) return;
            SwitchScreens(m_StartScreen);
            print("Screen Initialized");
            
            var _sceneCount = SceneManager.sceneCountInBuildSettings;  
            
            var scenes = new string[_sceneCount];
            
            for( var i = -1; i < _sceneCount; i++ )
            {
                var nextBuildIndex = SceneManager.GetActiveScene().buildIndex + i;
               print(nextBuildIndex); 
            }


        }

        // Update is called once per frame
        void Update()
        {

        }

        #endregion

        #region HelperMethods

        public void SwitchScreens(UI_Screen aScreen)
        {
            if (aScreen)
            {
                if (_currentScreen)
                {
                    _currentScreen.CloseScreen();
                    _previousScreen = _currentScreen;
                    print("Current Screen closed = " + _currentScreen.name);
                }
                _currentScreen = aScreen; 
                _currentScreen.gameObject.SetActive(true); 
                _currentScreen.StartScreen();
                print("New Screen = " + _currentScreen.name);
                onSwitchScreen?.Invoke();
            }
                
        }

        public void FadeIn()
        {
            if (ScreenFade)
            {
                ScreenFade.FadeScreenIn();
            }
            
        }

        public void FadeOut()
        {
            if (ScreenFade)
            {
                ScreenFade.FadeScreenOut();
            }

            
        }

        public void GoToPreviousScreen()
        {
            if (_previousScreen)
            {
                SwitchScreens(previousScreen);
            }
        }

        public void LoadScene(int sceneIndex)
        {
            SceneManager.LoadScene(sceneIndex);
        }

        IEnumerator WaitForLoadScene(int sceneIndex)
        {
            yield return null;
            
        }

        void InitializeScreens()
        {
            foreach (var screen in screens)
            {
                screen.gameObject.SetActive(true);
            }
        }

        public void Quit()
        {
            UnityEngine.Application.Quit();
        }

        #endregion
       
    }
}

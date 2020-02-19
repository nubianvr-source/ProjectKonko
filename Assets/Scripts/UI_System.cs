using System.Collections;
using System.Collections.Generic;
using System.Security.Permissions;
using Oculus.Platform;
using UnityEngine;
using UnityEngine.Assertions.Comparers;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NubianVR.UI
{


    public class UI_System : MonoBehaviour
    {
        [Header("Main Properties")] 
        public UI_Screen m_StartScreen;
        
        [Header("System Events")] public UnityEvent onSwitchScreen = new UnityEvent();

        [Header("Fade Reference")] 
        public OVRScreenFade ScreenFade;
        


        #region Variables

        private Component[] screens = new Component[0];

        private UI_Screen _currentScreen;
        public UI_Screen currentScreen => _currentScreen;

        private UI_Screen _previousScreen;
        public UI_Screen previousScreen => _previousScreen;

        #endregion

        #region MainMethods

        // Start is called before the first frame update
        void Start()
        {
            screens = GetComponentsInChildren<UI_Screen>(true);
            InitializeScreens();
            if (m_StartScreen)
            {
                SwitchScreens(m_StartScreen);
            }

            FadeIn();
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
                if (currentScreen)
                {
                    _currentScreen.CloseScreen();
                    _previousScreen = currentScreen;
                }

                _currentScreen = aScreen;
                _currentScreen.gameObject.SetActive(true);
                _currentScreen.StartScreen();

                onSwitchScreen?.Invoke();
            }

        }

        public void FadeIn()
        {
            ScreenFade.FadeScreenIn();
        }

        public void FadeOut()
        {
            ScreenFade.FadeScreenOut();
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
            StartCoroutine(WaitForLoadScene(sceneIndex));
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

        #endregion
       
    }
}

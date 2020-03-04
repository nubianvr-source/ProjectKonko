using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Konko.SceneManagement;
using UnityEngine.SceneManagement;


namespace Konko.UIManagement
{
    public class UIManager : MonoBehaviour
    {
        public UIScreen[] screens;
        public int initialScreenIndex = 0;
        public int currentScreenIndex = 0;

        //public SceneManager sceneManager;
        public OVRScreenFade screenFade;

        public bool allScreensHidden { get; private set; }

        // Start is called before the first frame update
        void Start()
        {

        }

        private void OnEnable()
        {
            ShowOnlyScreen(initialScreenIndex);
        }

        // Update is called once per frame
        void Update()
        {

        }


        void ShowOnlyScreen(int index)
        {
            if (index >= screens.Length) return;

            ShowOnlyScreenFadeOn(screens[index].name);
        }

        public void ShowOnlyScreenFadeOn(string name)
        {
            StartCoroutine(ShowOnlyFadeOn(name));
        }


        public void HideAllScreensFadeOn()
        {
            StartCoroutine(HideAllFadeOn());
        }



        IEnumerator ShowOnlyFadeOn(string name)
        {
            UIScreen targetScreen = Array.Find(screens, s => s.name == name);

            if (targetScreen == null) yield return null;

            HideAllScreensFadeOn();

            while (!allScreensHidden)
            {
                yield return new WaitForEndOfFrame();
            }

            targetScreen.Show();

            currentScreenIndex = Array.FindIndex(screens, s => s.name == name);

            screenFade.FadeScreenIn();
        }


        public void ShowOnlyScreenFadeOff(string name)
        {
            UIScreen targetScreen = Array.Find(screens, s => s.name == name);

            if (targetScreen == null) return;

            HideAllScreensFadeOff();
            targetScreen.Show();
        }

        public void HideAllScreensFadeOff()
        {
            Array.ForEach(screens, s => s.Hide());
        }



        IEnumerator HideAllFadeOn()
        {
            allScreensHidden = false;

            screenFade.FadeScreenOut();

            while (screenFade.screenIsActive)
            {
                yield return new WaitForEndOfFrame();
            }

            Array.ForEach(screens, s => s.Hide());

            allScreensHidden = true;
        }




        public void LoadNextScene(int sceneNumber)
        {
            //sceneManager.LoadScene(sceneNumber);
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneNumber);
        }
    }





    [Serializable]
    public class UIScreen
    {
        public string name;
        public GameObject[] panels;



        public void Show()
        {
            Array.ForEach(panels, p => p.SetActive(true));
        }


        public void Hide()
        {
            Array.ForEach(panels, p => p.SetActive(false));
        }
    }
}

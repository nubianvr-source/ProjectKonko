using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Konko.SceneManagement;

namespace Konko.UIManagement
{
    public class UIManager : MonoBehaviour
    {
        public UIScreen[] screens;
        public int initialScreenIndex = 0;
        public int currentScreenIndex = 0;

        public SceneManager sceneManager;
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

            ShowOnlyScreen(screens[index].name);
        }

        public void ShowOnlyScreen(string name)
        {
            StartCoroutine(ShowOnly(name));
        }

        public void HideAllScreens()
        {
            StartCoroutine(HideAll());
        }



        IEnumerator ShowOnly(string name)
        {
            UIScreen targetScreen = Array.Find(screens, s => s.name == name);

            if (targetScreen == null) yield return null;

            HideAllScreens();

            while (!allScreensHidden)
            {
                yield return new WaitForEndOfFrame();
            }

            targetScreen.Show();

            currentScreenIndex = Array.FindIndex(screens, s => s.name == name);

            screenFade.FadeScreenIn();
        }


        IEnumerator HideAll()
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




        public void LoadNextScene(string sceneName)
        {
            sceneManager.LoadScene(sceneName);
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

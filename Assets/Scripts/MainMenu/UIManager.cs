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

            currentScreenIndex = index;

            HideAllScreens();
            screens[index].Show();
        }

        public void ShowOnlyScreen(string name)
        {
            UIScreen targetScreen = Array.Find(screens, s => s.name == name);

            if (targetScreen == null) return;

            HideAllScreens();
            targetScreen.Show();
        }

        public void HideAllScreens()
        {
            Array.ForEach(screens, s => s.Hide());
        }




        public void LoadNextScene(string sceneName)
        {
            sceneManager.LoadScene(sceneName, fade: true);
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

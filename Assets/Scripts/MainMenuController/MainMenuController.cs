using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public GameObject[] screens;
    public string startScreenName;

    void Start() => SwitchToScreen(startScreenName);

    public void SwitchToScreen(string screenGameObjectName)
    {
        GameObject screen = Array.Find(screens, screenInArray => screenInArray.name == screenGameObjectName);

        if (screen)
        {
            Array.ForEach(screens, screenInArray => screenInArray.SetActive(false));
            screen.SetActive(true);
        }
    }

    public void SwitchToScreen(int screenIndex)
    {
        GameObject screen = screenIndex < screens.Length - 1 ? screens[screenIndex] : null;

        if (screen)
        {
            Array.ForEach(screens, screenInArray => screenInArray.SetActive(false));
            screen.SetActive(true);
        }
    }

    public void DisableAllScreens()
    {
        Array.ForEach(screens, screenInArray => screenInArray.SetActive(false));
    }

    public void LoadLesson(int lessonBuildIndex)
    {
        DisableAllScreens();
        SceneManager.LoadSceneAsync(lessonBuildIndex, LoadSceneMode.Single);
    }

    public void LoadLesson(string lessonSceneName)
    {
        DisableAllScreens();
        SceneManager.LoadSceneAsync(lessonSceneName, LoadSceneMode.Single);
    }
}

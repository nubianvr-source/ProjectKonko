using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public GameObject[] menuScreens;
    public GameObject mainGame;
    public int initialScreenIndex = 0;
    public int currentScreenIndex = 0;

    void OnEnable()
    {
        UnloadMainGame();
        ShowOnlyScreen(initialScreenIndex);
    }

    void ShowOnlyScreen(int index)
    {
        if (index >= menuScreens.Length) return;

        currentScreenIndex = index;

        HideAllScreens();
        menuScreens[index].SetActive(true);
    }

    public void ShowOnlyScreen(string name)
    {
        GameObject targetScreen = null;

        for (int i = 0; i < menuScreens.Length; i++)
        {
            if(menuScreens[i].name == name)
            {
                targetScreen = menuScreens[i];
                currentScreenIndex = i;
            }
        }

        if (!targetScreen) return;

        HideAllScreens();
        targetScreen.SetActive(true);
    }

    public void HideAllScreens()
    {
        Array.ForEach(menuScreens, screen => screen.SetActive(false));
    }

    public void LoadLesson(int buildIndex)
    {
        SceneManager.LoadScene(buildIndex, LoadSceneMode.Single);
    }

    public void LoadLesson(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    public void LoadMainGame()
    {
        mainGame.SetActive(true);
        HideAllScreens();
    }


    public void UnloadMainGame()
    {
        mainGame.SetActive(false);
        ShowOnlyScreen(currentScreenIndex);
    }
}

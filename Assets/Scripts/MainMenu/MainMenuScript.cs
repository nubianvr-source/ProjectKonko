using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public GameObject[] menuScreens;
    public int initialScreenIndex = 0;

    void OnEnable()
    {
        ShowOnlyScreen(initialScreenIndex);
    }

    void ShowOnlyScreen(int index)
    {
        if (index >= menuScreens.Length) return;

        Array.ForEach(menuScreens, screen => screen.SetActive(false));
        menuScreens[index].SetActive(true);
    }

    public void ShowOnlyScreen(string name)
    {
        GameObject targetScreen = Array.Find(menuScreens, screen => screen.name == name);

        if (!targetScreen) return;

        Array.ForEach(menuScreens, screen => screen.SetActive(false));
        targetScreen.SetActive(true);
    }

    public void LoadLesson(int buildIndex)
    {
        SceneManager.LoadScene(buildIndex, LoadSceneMode.Single);
    }

    public void LoadLesson(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
}

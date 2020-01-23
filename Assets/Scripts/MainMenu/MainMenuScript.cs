using System;
using UnityEngine;
using Konko.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public GameObject[] menuScreens;
    public int initialScreenIndex = 0;
    public int currentScreenIndex = 0;

    public SceneManager sceneManager;

    void OnEnable()
    {
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


    public void LoadLesson(int number)
    {
        sceneManager.LoadScene("lesson_" + number);
    }
}

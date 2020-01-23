using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

namespace Konko.SceneManagement
{
    public class SceneManager : MonoBehaviour
    {
        public Scene[] scenes;
        public string primarySceneName;
        //public VideoPlayer videoPlayer;

        public bool allScenesUnloaded { get; private set; } = true;

        Skybox mainSkybox;
        OVRScreenFade screenFade;

        List<string> loadedScenes = new List<string>();
        string activeScene;


        private void Awake()
        {
            mainSkybox = FindObjectOfType<Skybox>();
            screenFade = FindObjectOfType<OVRScreenFade>();
        }

        private void Start()
        {
            LoadScene(primarySceneName);
        }



        public void LoadScene(string name, LoadMode mode = LoadMode.Single)
        {
            StartCoroutine(Load(name, mode));
        }

        public void LoadScene(int index, LoadMode mode = LoadMode.Single)
        {
            LoadScene(scenes[index].name, mode);
        }

        public void UnloadScene(string name, bool useSceneFadeOut = true)
        {
            StartCoroutine(Unload(name, useSceneFadeOut));
        }

        public void UnloadAllScenes()
        {
            StartCoroutine(UnloadAll());
        }



        void StageScene(Scene scene)
        {
            if(scene.skyboxMaterial) mainSkybox.material = scene.skyboxMaterial;
        }




        public IEnumerator Load(string name, LoadMode mode)
        {
            Scene scene = Array.Find(scenes, s => s.name == name);

            if (scene == null) yield return null;

            switch (mode)
            {
                case LoadMode.Single:
                    UnloadAllScenes();

                    while (!allScenesUnloaded)
                    {
                        yield return new WaitForEndOfFrame();
                    }

                    scene.Load();
                    break;
                case LoadMode.Additive:
                    scene.Load();
                    break;
            }

            if (!loadedScenes.Contains(name)) loadedScenes.Add(name);

            activeScene = name;

            if (scene.fadeIn) screenFade.FadeScreenIn();

            StageScene(scene);

            print("scene " + scene.name + " has been loaded.");
        }


        public IEnumerator Unload(string name, bool useSceneFadeOut = true)
        {
            Scene scene = Array.Find(scenes, s => s.name == name);

            if (scene == null) yield return null;

            if (useSceneFadeOut && scene.fadeOut) screenFade.FadeScreenOut();

            while (useSceneFadeOut && scene.fadeOut && screenFade.screenIsActive)
            {
                yield return new WaitForEndOfFrame();
            }

            scene.Unload();

            if (loadedScenes.Contains(name)) loadedScenes.Remove(name);

            if (activeScene == name)
            {
                if (loadedScenes.Count > 0) activeScene = loadedScenes[loadedScenes.Count - 1];
                else activeScene = "";
            }

            print("scene " + scene.name + " has been unloaded.");
        }

        public IEnumerator UnloadAll()
        {
            allScenesUnloaded = false;

            Scene active = Array.Find(scenes, s => s.name == activeScene);

            if (active != null && active.fadeOut) screenFade.FadeScreenOut();

            while (active != null && active.fadeOut && screenFade.screenIsActive)
            {
                yield return new WaitForEndOfFrame();
            }

            Array.ForEach(scenes, s => UnloadScene(s.name, false));

            allScenesUnloaded = true;
        }
    }



    [Serializable]
    public class Scene
    {
        public string name;
        public GameObject entity;
        public Material skyboxMaterial;

        public bool fadeOut;
        public bool fadeIn;



        public void Load()
        {
            
            entity.SetActive(true);
        }

        public void Unload()
        {
            
            entity.SetActive(false);
        }
    }




    public enum LoadMode
    {
        Single = 0,
        Additive = 1
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Konko.SceneManagement
{
    public class SceneManager : MonoBehaviour
    {
        public Scene[] scenes;
        public string primarySceneName;

        Skybox mainSkybox;
        OVRScreenFade screenFade;

        List<string> loadedScenes = new List<string>();
        string activeScene;


        private void Awake()
        {
            mainSkybox = FindObjectOfType<Skybox>();
            screenFade = FindObjectOfType<OVRScreenFade>();

            LoadScene(primarySceneName, fade: true);
        }

        // Update is called once per frame
        void Update()
        {
            
        }


        
        public void LoadScene(string name, LoadMode mode = LoadMode.Single, bool fade = false)
        {
            Scene scene = Array.Find(scenes, s => s.name == name);

            if (scene == null) return;

            switch (mode)
            {
                case LoadMode.Single:
                    UnloadAllScenes();
                    scene.Load();
                    break;
                case LoadMode.Additive:
                    scene.Load();
                    break;
            }

            if (!loadedScenes.Contains(name)) loadedScenes.Add(name);

            activeScene = name;

            if(fade) screenFade.FadeIn();

            StageScene(scene);

            print("scene " + scene.name + " has been loaded.");
        }

        public void LoadScene(int index, LoadMode mode = LoadMode.Single, bool fade = false)
        {
            LoadScene(scenes[index].name, mode, fade);
        }

        public void UnloadScene(string name)
        {
            Scene scene = Array.Find(scenes, s => s.name == name);

            if (scene == null) return;

            scene.Unload();
            if (loadedScenes.Contains(name)) loadedScenes.Remove(name);

            if (activeScene == name)
            {
                if (loadedScenes.Count > 0) activeScene = loadedScenes[loadedScenes.Count - 1];
                else activeScene = "";
            }
        }

        public void UnloadAllScenes()
        {
            Array.ForEach(scenes, s => UnloadScene(s.name));
        }



        void StageScene(Scene scene)
        {
            if(scene.skyboxMaterial) mainSkybox.material = scene.skyboxMaterial;
        }
    }



    [Serializable]
    public class Scene
    {
        public string name;
        public GameObject entity;
        public Material skyboxMaterial;



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

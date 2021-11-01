using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Assets.Script.Common.Component;
using Assets.Script.Scene;

namespace Assets.Script.Common.Scene
{
    /// <summary>
    /// ÉVÅ[Éìä«óù
    /// </summary>
    public class SceneManager : SingletonMonoBehaviour<SceneManager>
    {
        [SerializeField]
        private Transform sceneParent = default;

        private const string ScenePrefabRoot = "Prefabs/Scene/";

        private SceneBase currentScene = null;

        public void Start()
        {
            SceneDataBase startSceneData = new TitleSceneData();
            string path = ScenePrefabRoot + startSceneData.ScenePrefab;
            var obj = Resources.Load(path) as GameObject;
            currentScene = Instantiate(obj, sceneParent).GetComponent<SceneBase>();
            currentScene.OnAppearPrep();
            FadeManager.Instance.FadeOut(() =>
            {
                currentScene.OnViewAppear();
            });
        }


        public void NextScene(SceneDataBase sceneData)
        {
            currentScene.OnHide();
            FadeManager.Instance.FadeIn(()=> {
                string path = ScenePrefabRoot + sceneData.ScenePrefab;
                var obj = Resources.Load(path) as GameObject;
                var scene = Instantiate(obj, sceneParent).GetComponent<SceneBase>();
                scene.gameObject.SetActive(true);
                scene.OnAppearPrep();
                currentScene.OnSceneRelease();
                Destroy(currentScene.gameObject);
                currentScene = scene;
                FadeManager.Instance.FadeOut(() =>
                {
                    currentScene.OnViewAppear();
                });
            });
        }
    }
}
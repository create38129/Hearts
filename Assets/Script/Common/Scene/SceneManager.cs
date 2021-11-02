using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Assets.Script.Common.Component;
using Assets.Script.Scene;
using Cysharp.Threading.Tasks;

namespace Assets.Script.Common.Scene
{
    /// <summary>
    /// シーン管理
    /// </summary>
    public class SceneManager : SingletonMonoBehaviour<SceneManager>
    {
        [SerializeField]
        private Transform sceneParent = default;
        
        /// <summary>
        /// 現在のシーン
        /// </summary>
        public ReadOnlyReactiveProperty<SceneBase> CurrentScene => currentScene.ToReadOnlyReactiveProperty();
        private readonly ReactiveProperty<SceneBase> currentScene = new ReactiveProperty<SceneBase>(null);

        private const string ScenePrefabRoot = "Prefabs/Scene/";

        private readonly List<SceneBase> sceneHistory = new List<SceneBase>();

        public async void Start()
        {
            //最初のシーン呼び出し
            SceneDataBase startSceneData = new TitleSceneData();
            string path = ScenePrefabRoot + startSceneData.ScenePrefab;
            var obj = Resources.Load(path) as GameObject;
            currentScene.Value = Instantiate(obj, sceneParent).GetComponent<SceneBase>();
            await currentScene.Value.OnAppearPrep();

            await FadeManager.Instance.FadeOut();
            currentScene.Value.OnViewAppear();
        }


        public async void NextScene(SceneDataBase sceneData, bool canBack = true)
        {
            currentScene.Value.OnHidePrep();
            await FadeManager.Instance.FadeIn();

            string path = ScenePrefabRoot + sceneData.ScenePrefab;
            GameObject obj = await Resources.LoadAsync(path) as GameObject;
            var scene = Instantiate(obj, sceneParent).GetComponent<SceneBase>();
            scene.gameObject.SetActive(true);
            scene.OnInitialize();
            await scene.OnAppearPrep();

            if (canBack)
            {
                sceneHistory.Add(currentScene.Value);
                currentScene.Value.gameObject.SetActive(false);
            }
            else
            {
                await currentScene.Value.OnSceneRelease();
                Destroy(currentScene.Value.gameObject);
            }

            currentScene.Value = scene;
            await FadeManager.Instance.FadeOut();

            currentScene.Value.OnViewAppear();
        }


        public async void BackScene()
        {
            //戻れないときは何もしない(そもそも来るな)
            if(sceneHistory.Count == 0) {
                Debug.LogError("Can not BackScene : sceneHistory count 0");
                return;
            }

            currentScene.Value.OnHidePrep();

            await FadeManager.Instance.FadeIn();

            await currentScene.Value.OnSceneRelease();
            Destroy(currentScene.Value.gameObject);

            currentScene.Value = sceneHistory.Last();
            sceneHistory.Remove(currentScene.Value);
            currentScene.Value.gameObject.SetActive(true);
            await currentScene.Value.OnAppearPrep();

            await FadeManager.Instance.FadeOut();

            currentScene.Value.OnViewAppear();
        }
    }
}
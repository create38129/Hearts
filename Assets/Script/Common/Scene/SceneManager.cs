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
        public ReadOnlyReactiveProperty<SceneModelBase> CurrentScene => currentScene.ToReadOnlyReactiveProperty();
        private readonly ReactiveProperty<SceneModelBase> currentScene = new ReactiveProperty<SceneModelBase>(null);

        private const string ScenePrefabRoot = "Prefabs/Scene/";

        private readonly List<SceneModelBase> sceneHistory = new List<SceneModelBase>();

        public async void Start()
        {
            //最初のシーン呼び出し
            SceneModelBase model = new TitleSceneModel();
            string path = ScenePrefabRoot + model.ScenePrefab;
            var obj = Resources.Load(path) as GameObject;
            currentScene.Value = model;
            ScenePresenterBase presenter = Instantiate(obj, sceneParent).GetComponent<ScenePresenterBase>();
            presenter.Initialize(model);
            presenter.Register();
            currentScene.Value.SetActive(true);
            await currentScene.Value.AppearPrep();

            await FadeManager.Instance.FadeOut();
            currentScene.Value.ViewAppear();
        }


        public async void NextScene(SceneModelBase model, bool canBack = true)
        {
            currentScene.Value.HidePrep();
            await FadeManager.Instance.FadeIn();

            string path = ScenePrefabRoot + model.ScenePrefab;
            GameObject obj = await Resources.LoadAsync(path) as GameObject;
            ScenePresenterBase presenter = Instantiate(obj, sceneParent).GetComponent<ScenePresenterBase>();
            presenter.Initialize(model);
            presenter.Register();
            model.SetActive(true);
            model.Initialize();
            await model.AppearPrep();

            if (canBack)
            {
                sceneHistory.Add(currentScene.Value);
                currentScene.Value.SetActive(false);
            }
            else
            {
                await currentScene.Value.SceneRelease();
            }

            currentScene.Value = model;
            await FadeManager.Instance.FadeOut();

            currentScene.Value.ViewAppear();
        }


        public async void BackScene()
        {
            //戻れないときは何もしない(そもそも来るな)
            if(sceneHistory.Count == 0) {
                Debug.LogError("Can not BackScene : sceneHistory count 0");
                return;
            }

            currentScene.Value.HidePrep();

            await FadeManager.Instance.FadeIn();

            await currentScene.Value.SceneRelease();

            currentScene.Value = sceneHistory.Last();
            sceneHistory.Remove(currentScene.Value);
            currentScene.Value.SetActive(true);
            await currentScene.Value.AppearPrep();

            await FadeManager.Instance.FadeOut();

            currentScene.Value.ViewAppear();
        }
    }
}
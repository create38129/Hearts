using Assets.Script.Common.Component;
using Assets.Script.Common.Scene;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace Assets.Script.Scene
{

    public class HomeSceneModel : SceneModelBase
    {
        public override string SceneName => "ホーム";
        public override string ScenePrefab => "HomeScene";

        public void NextScene()
        {
            SceneManager.Instance.BackScene();
        }
    }
}

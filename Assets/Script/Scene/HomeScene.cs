using Assets.Script.Common.Component;
using Assets.Script.Common.Scene;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace Assets.Script.Scene
{
    public class HomeSceneData : SceneDataBase
    {
        public override string ScenePrefab => "HomeScene";
    }

    public class HomeScene : SceneBase
    {
        public override string SceneName => "ホーム";

        [SerializeField]
        private ButtonComponent buttoncomp = default;

        public override void OnInitialize()
        {
            buttoncomp.OnClick.Subscribe(_ =>
            {
                SceneManager.Instance.BackScene();
            }).AddTo(this);
        }
    }
}

using Assets.Script.Common.Scene;
using UnityEngine;
using UniRx;
using Assets.Script.Common.Component;

namespace Assets.Script.Scene
{

    public class TitleSceneModel : SceneModelBase
    {
        public override string SceneName => "Title";
        public override string ScenePrefab => "TitleScene";

        public void NextScene()
        {
            SceneManager.Instance.NextScene(new HomeSceneModel());
        }
    }
}

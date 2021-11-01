using Assets.Script.Common.Scene;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Assets.Script.Common;
using Assets.Script.Common.Component;

namespace Assets.Script.Scene
{
    public class TitleSceneData : SceneDataBase
    {
        public override string ScenePrefab => "TitleScene";
    }

    public class TitleScene : SceneBase
    {
        public override string SceneName => "Title";



        [SerializeField]
        private ButtonComponent buttoncomp = default;


        private void Start()
        {
            buttoncomp.OnClick.Subscribe(_ =>
            {
                SceneManager.Instance.NextScene(new HomeSceneData());
                //FadeManager.Instance.FadeIn();
            }).AddTo(this);
        }
    }
}

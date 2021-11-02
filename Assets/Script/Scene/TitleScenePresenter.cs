using Assets.Script.Common.Component;
using Assets.Script.Common.Scene;
using UnityEngine;
using UniRx;

namespace Assets.Script.Scene
{
    class TitleScenePresenter : ScenePresenterBase
    {
        [SerializeField]
        private ButtonComponent buttoncomp = default;


        public override void Register()
        {
            buttoncomp.OnClick.Subscribe(_ =>
            {
                ((TitleSceneModel)model).NextScene();
            }).AddTo(this);

            base.Register();
        }
    }
}

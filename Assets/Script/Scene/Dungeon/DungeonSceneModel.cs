using Assets.Script.Common.Scene;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Script.Scene.Dungeon
{
    class DungeonSceneModel : SceneModelBase
    {
        public override string SceneName => "ダンジョン";

        public override string ScenePrefab => "DungeonScene";

        public CharacterModel CharacterModel => characterModel;
        private readonly CharacterModel characterModel = new CharacterModel();

        public override void Initialize()
        {
            base.Initialize();

            characterModel.Initialize();
            characterModel.Register();
        }

        public override UniTask SceneRelease()
        {
            characterModel.UnRegister();
            return base.SceneRelease();
        }

        public void Update()
        {
            characterModel.Update();
        }
    }
}

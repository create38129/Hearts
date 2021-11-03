using Assets.Script.Common.Scene;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Script.Scene.Dungeon
{
    class DungeonScenePresenter : ScenePresenterBase
    {
        [SerializeField]
        private CharacterPresenter characterPresenter = default;

        public override void Initialize(SceneModelBase model)
        {
            base.Initialize(model);
            characterPresenter.Initialize(((DungeonSceneModel)model).CharacterModel);
        }

        public override void Register()
        {
            base.Register();
            characterPresenter.Register();
        }

        public void Update()
        {
            ((DungeonSceneModel)model).Update();
        }
    }
}

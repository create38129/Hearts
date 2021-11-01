using Assets.Script.Common.Scene;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Script.Scene
{
    public class HomeSceneData : SceneDataBase
    {
        public override string ScenePrefab => "HomeScene";
    }

    public class HomeScene : SceneBase
    {
        public override string SceneName => "ホーム";
    }
}

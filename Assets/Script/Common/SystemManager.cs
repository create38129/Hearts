using Assets.Script.Common.Component;
using Assets.Script.Hid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Script.Common
{
    /// <summary>
    /// システム系管理クラス
    /// </summary>
    class SystemManager : SingletonMonoBehaviour<SystemManager>
    {
        [SerializeField]
        private HidPresenter hidPresenter = default;

        public IHid Hid => hidModel;

        private readonly HidModel hidModel = new HidModel();

        public void Start()
        {
            hidPresenter.Initialize(hidModel);
        }
    }
}

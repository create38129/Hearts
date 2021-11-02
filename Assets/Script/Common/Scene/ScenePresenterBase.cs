using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace Assets.Script.Common.Scene
{
    /// <summary>
    /// シーンの表示を司る基底クラス
    /// </summary>
    class ScenePresenterBase : MonoBehaviour
    {
        protected SceneModelBase model;

        public virtual void Initialize(SceneModelBase model)
        {
            this.model = model;
        }

        public virtual void Register()
        {
            model.IsActive.Subscribe(gameObject.SetActive).AddTo(this);
            model.OnRelease.Subscribe(_ => Destroy(gameObject)).AddTo(this);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;

namespace Assets.Script.Common.Scene
{
    /// <summary>
    /// シーンベース
    /// </summary>
    public abstract class SceneBase : MonoBehaviour
    {
        /// <summary>
        /// シーン名
        /// </summary>
        public abstract string SceneName { get; }

        /// <summary>
        /// シーンに必要なデータ
        /// </summary>
        protected SceneDataBase sceneData;

        /// <summary>
        /// シーンデータの設定
        /// </summary>
        /// <param name="data"></param>
        public void SetSceneData(SceneDataBase data)
        {
            sceneData = data;
        }

        /// <summary>
        /// 初期化処理
        /// </summary>
        public virtual void OnInitialize()
        {
            // ボタン登録とか
        }

        /// <summary>
        /// 暗転中準備
        /// </summary>
        public virtual UniTask OnAppearPrep()
        {
            // ロードとか

            return new UniTask();
        }

        /// <summary>
        /// 暗転解除後
        /// </summary>
        public virtual void OnViewAppear()
        {
            // 画面演出
        }

        /// <summary>
        /// 暗転開始
        /// </summary>
        public virtual void OnHidePrep()
        {
        }


        /// <summary>
        /// シーン破棄
        /// </summary>
        public virtual UniTask OnSceneRelease()
        {
            //リソース解放等
            return new UniTask();
        }


    }
}
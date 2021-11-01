using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;


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
        /// 暗転中準備
        /// </summary>
        public virtual void OnAppearPrep()
        {
            // ロードとか
        }

        /// <summary>
        /// 暗転解除
        /// </summary>
        public virtual void OnViewAppear()
        {

        }

        /// <summary>
        /// 暗転開始
        /// </summary>
        public virtual void OnHide()
        {
        }


        /// <summary>
        /// シーン破棄
        /// </summary>
        public virtual void OnSceneRelease()
        {
        }


    }
}
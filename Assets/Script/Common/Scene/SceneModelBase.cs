using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using System;

namespace Assets.Script.Common.Scene
{
    /// <summary>
    /// シーンベース
    /// </summary>
    public abstract class SceneModelBase
    {
        /// <summary>
        /// シーン名
        /// </summary>
        public abstract string SceneName { get; }

        public abstract string ScenePrefab { get; }

        /// <summary>
        /// シーンがアクティブ常態か
        /// </summary>
        public ReadOnlyReactiveProperty<bool> IsActive => isActive.ToReadOnlyReactiveProperty();
        private readonly ReactiveProperty<bool> isActive = new ReactiveProperty<bool>(false);

        /// <summary>
        /// シーンが解放される時に通知
        /// </summary>
        public IObservable<Unit> OnRelease => onRelease;
		private readonly Subject<Unit> onRelease = new Subject<Unit>();


        /// <summary>
        /// 初期化処理
        /// </summary>
        public virtual void Initialize()
        {
        }

        /// <summary>
        /// 暗転中準備
        /// </summary>
        public virtual UniTask AppearPrep()
        {
            // ロードとか

            return new UniTask();
        }

        /// <summary>
        /// 暗転解除後
        /// </summary>
        public virtual void ViewAppear()
        {
            // 画面演出
        }

        /// <summary>
        /// 暗転開始
        /// </summary>
        public virtual void HidePrep()
        {
        }


        /// <summary>
        /// シーン破棄
        /// </summary>
        public virtual UniTask SceneRelease()
        {
            onRelease.OnNext(Unit.Default);

            //リソース解放等
            return new UniTask();
        }

        public void SetActive(bool isActive)
        {
            this.isActive.Value = isActive;
        }
    }
}
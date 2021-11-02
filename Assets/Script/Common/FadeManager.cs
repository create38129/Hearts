using Assets.Script.Common.Component;
using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Script.Common
{
    /// <summary>
    /// à√ì]ä«óù
    /// </summary>
    public class FadeManager : SingletonMonoBehaviour<FadeManager>
    {
        [SerializeField]
        private Image fadeImage = default;

        [SerializeField]
        private float duration = 0.5f;

        private readonly ReactiveProperty<bool> isRaycast = new ReactiveProperty<bool>();
        private readonly ReactiveProperty<float> alpha = new ReactiveProperty<float>();


        private IDisposable disposable = null;

        private void Start()
        {
            isRaycast.Subscribe(raycast => fadeImage.raycastTarget = raycast).AddTo(this);
            alpha.Subscribe(fadeImage.SetAlpha).AddTo(this);
        }


        public async UniTask FadeIn()
        {
            isRaycast.Value = true;
            bool isWait = true;
            if (disposable != null) disposable.Dispose();

            var subject = new Subject<float>();
            subject.Subscribe(
                ratio => {
                    alpha.Value = ratio;
                },
                ()=> {
                    isWait = false;
                })
                .AddTo(this);
            disposable = Extensions.Tween(subject, duration);

            await UniTask.WaitUntil(() => isWait);
        }

        public async UniTask FadeOut()
        {
            if (disposable != null) disposable.Dispose();

            bool isWait = true;
            var subject = new Subject<float>();
            subject.Subscribe(
                ratio => {
                    alpha.Value = 1.0f - ratio;
                },
                () =>
                {
                    isRaycast.Value = false;
                    isWait = false;
                })
                .AddTo(this);
            disposable = Extensions.Tween(subject, duration);
            while (isWait)
            {
                await UniTask.Yield();
            }
        }
    }

}
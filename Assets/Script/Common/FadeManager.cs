using Assets.Script.Common.Component;
using System;
using System.Collections;
using System.Collections.Generic;
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


        public IObservable<Unit> OnFadeIn => fadeInSubject;
        private Subject<Unit> fadeInSubject = new Subject<Unit>();

        public IObservable<Unit> OnFadeOut => fadeOutSubject;
        private Subject<Unit> fadeOutSubject = new Subject<Unit>();

        private IDisposable disposable = null;

        public void FadeIn(Action action = null)
        {
            fadeImage.raycastTarget = true;
            
            if (disposable != null) disposable.Dispose();

            var subject = new Subject<float>();
            subject.Subscribe(
                ratio => {
                    fadeImage.SetAlpha(ratio);
                },
                ()=> {
                    fadeInSubject.OnNext(Unit.Default);
                    if(action != null) action();
                })
                .AddTo(this);
            disposable = Extensions.Tween(subject, duration);
        }

        public void FadeOut(Action action = null)
        {
            if (disposable != null) disposable.Dispose();

            var subject = new Subject<float>();
            subject.Subscribe(
                ratio => {
                    fadeImage.SetAlpha(1.0f - ratio);
                },
                () =>
                {
                    fadeImage.raycastTarget = false;
                    fadeOutSubject.OnNext(Unit.Default);
                    if (action != null) action();
                })
                .AddTo(this);
            disposable = Extensions.Tween(subject, duration);
        }
    }

}
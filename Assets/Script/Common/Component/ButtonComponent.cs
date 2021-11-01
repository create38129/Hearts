using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Script.Common.Component
{
    /// <summary>
    /// ボタン
    /// </summary>
    [RequireComponent(typeof(Graphic))]
    class ButtonComponent : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
    {
        [SerializeField]
        private Text buttonText = default;

        [SerializeField]
        private bool isScale = true;

        [SerializeField]
        private bool isChangeColor = true;

        [SerializeField]
        private bool isLongTap = false;

        private const float longTapDuration = 1.0f;
        private const float animDuration = 0.1f;
        private const float scaleSize = 0.9f;

        private static readonly Color pressdColor = new Color(0.8f, 0.8f, 0.8f, 1f);
        private static readonly Color disabledColor = new Color(0.8f, 0.8f, 0.8f, 1f);

        private IDisposable animDisposable = null;
        private IDisposable longTapDisposable = null;

        /// <summary>
        /// 色を変更するためのリスト
        /// </summary>
        private readonly Dictionary<Graphic, Color> graphics = new Dictionary<Graphic, Color>();

        /// <summary>
        /// 押されている
        /// </summary>
        public ReadOnlyReactiveProperty<bool> IsPress => isPress.ToReadOnlyReactiveProperty();
        private readonly ReactiveProperty<bool> isPress = new ReactiveProperty<bool>(false);

        /// <summary>
        /// クリックを通知
        /// </summary>
        public IObservable<Unit> OnClick => onClick;
        private readonly Subject<Unit> onClick = new Subject<Unit>();

        /// <summary>
        /// 長押しを通知
        /// </summary>
        public IObservable<Unit> OnLongTap => onLongTap;
        private Subject<Unit> onLongTap = new Subject<Unit>();


        public void Start()
        {
            foreach(var graphic in gameObject.GetComponentsInChildren<Graphic>())
            {
                graphics.Add(graphic, graphic.color);
            }
        }

        private void Press()
        {
            if (isPress.Value) return;

            isPress.Value = true;
            if (longTapDisposable != null) longTapDisposable.Dispose();
            if (isLongTap) longTapDisposable = Observable.Timer(TimeSpan.FromSeconds(longTapDuration)).Subscribe(_ => LongTap()).AddTo(this);

            if (animDisposable != null) animDisposable.Dispose();
            var subject = new Subject<float>();
            foreach (var graphic in graphics)
            {
                if(isScale) graphic.Key.transform.localScale = new Vector3(1f, 1f, 1f);
                if (isChangeColor) graphic.Key.SetColor(Color.white);
            }
            subject.Subscribe(
                ratio => {
                    var scale = 1.0f - (1.0f - scaleSize) * ratio;
                    foreach(var graphic in graphics)
                    {
                        var color = graphic.Value - (Color.white - pressdColor) * ratio;
                        color.a = graphic.Value.a;
                        if (isScale) graphic.Key.transform.localScale = new Vector3(scale, scale, scale);
                        if (isChangeColor) graphic.Key.SetColor(color);
                    }
                })
                .AddTo(this);
            animDisposable = Extensions.Tween(subject, animDuration);
        }


        private void Release()
        {
            if (!isPress.Value) return;

            isPress.Value = false;
            if (longTapDisposable != null) longTapDisposable.Dispose();
            if (animDisposable != null) animDisposable.Dispose();
            foreach (var graphic in graphics)
            {
                if (isScale) graphic.Key.transform.localScale = new Vector3(scaleSize, scaleSize, scaleSize);
            }
            var subject = new Subject<float>();
            subject.Subscribe(
                ratio => {
                    var scale = 1.0f - (1.0f - scaleSize) * (1.0f - ratio);
                    foreach (var graphic in graphics)
                    {
                        var color = graphic.Value - (Color.white - pressdColor) * (1.0f - ratio);
                        color.a = graphic.Value.a;
                        if (isScale) graphic.Key.transform.localScale = new Vector3(scale, scale, scale);
                        if (isChangeColor) graphic.Key.SetColor(color);
                    }
                })
                .AddTo(this);
            animDisposable = Extensions.Tween(subject, animDuration);
        }

        private void LongTap()
        {
            if (longTapDisposable != null) longTapDisposable.Dispose();
            onLongTap.OnNext(Unit.Default);
            Release();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Press();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (isPress.Value)
            {
                onClick.OnNext(Unit.Default);
            }
            Release();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            Release();
        }

        public void SetText(string text)
        {
            if (buttonText != null)
            {
                buttonText.text = text;
            }
        }

        public string GetText()
        {
            if (buttonText == null) return String.Empty;

            return buttonText.text;
        }
    }
}

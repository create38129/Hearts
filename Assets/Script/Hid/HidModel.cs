using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace Assets.Script.Hid
{
    /// <summary>
    /// 入力管理モデル
    /// </summary>
    class HidModel : IHid
    {
        public IObservable<float> LStickHorizontalAxis => lStickHorizontalAxis.ToReadOnlyReactiveProperty();
        private readonly Subject<float> lStickHorizontalAxis = new Subject<float>();

        public IObservable<float> LStickVerticalAxis => lStickVerticalAxis.ToReadOnlyReactiveProperty();
        private readonly Subject<float> lStickVerticalAxis = new Subject<float>();

        public IObservable<float> RStickHorizontalAxis => rStickHorizontalAxis.ToReadOnlyReactiveProperty();
        private readonly Subject<float> rStickHorizontalAxis = new Subject<float>();

        public IObservable<float> RStickVerticalAxis => rStickVerticalAxis.ToReadOnlyReactiveProperty();
        private readonly Subject<float> rStickVerticalAxis = new Subject<float>();

        public IObservable<Vector2> Move => move.ToReadOnlyReactiveProperty();
        private readonly Subject<Vector2> move = new Subject<Vector2>();

        public void OnLStickHorizontalAxis(float axis)
        {
            if(!Mathf.Approximately(axis, 0f)) lStickHorizontalAxis.OnNext(axis);
        }

        public void OnLStickVerticalAxis(float axis)
        {
            if (!Mathf.Approximately(axis, 0f)) lStickVerticalAxis.OnNext(axis);
        }

        public void OnRStickHorizontalAxis(float axis)
        {
            if (!Mathf.Approximately(axis, 0f)) rStickHorizontalAxis.OnNext(axis);
        }

        public void OnRStickVerticalAxis(float axis)
        {
            if (!Mathf.Approximately(axis, 0f)) rStickVerticalAxis.OnNext(axis);
        }

        public void SetMove(Vector2 move)
        {
            this.move.OnNext(move);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace Assets.Script.Hid
{
    interface IHid
    {
        public IObservable<Vector2> Move { get; }
        public IObservable<float> LStickHorizontalAxis { get; }
        public IObservable<float> LStickVerticalAxis { get; }
        public IObservable<float> RStickHorizontalAxis { get; }
        public IObservable<float> RStickVerticalAxis { get; }
    }
}

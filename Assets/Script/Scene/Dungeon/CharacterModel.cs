using Assets.Script.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace Assets.Script.Scene.Dungeon
{
    class CharacterModel
    {
        public ReadOnlyReactiveProperty<Vector3> Pos => pos.ToReadOnlyReactiveProperty();
        private readonly ReactiveProperty<Vector3> pos = new ReactiveProperty<Vector3>(Vector3.zero);
		
        public ReadOnlyReactiveProperty<float> Rotation => rotation.ToReadOnlyReactiveProperty();
        private readonly ReactiveProperty<float> rotation = new ReactiveProperty<float>(0f);


        private const float speed = 5f;
        private Vector3 movingPos = Vector3.zero;

        private CompositeDisposable disposables = new CompositeDisposable();

        public void Initialize()
        {
        }

        public void Register()
        {
            //SystemManager.Instance.Hid.LStickHorizontalAxis.Subscribe(axis => movingPos.x += axis).AddTo(disposables);
            //SystemManager.Instance.Hid.LStickVerticalAxis.Subscribe(axis => movingPos.y += axis).AddTo(disposables);

            SystemManager.Instance.Hid.Move.Subscribe(move => {
                movingPos = move;
                Debug.LogError(move);
            }).AddTo(disposables);
        }

        public void UnRegister()
        {
            disposables.Clear();
        }


        public void Update()
        {
            float elapsed = Time.deltaTime;

            var posValue = pos.Value;
            posValue += movingPos * speed;
            pos.Value = posValue;

            movingPos = Vector3.zero;
        }
    }
}

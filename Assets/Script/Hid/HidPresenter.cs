﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Script.Hid
{
    class HidPresenter : MonoBehaviour
    {

        private HidModel model;

        private Input moveAction;

        public void Initialize(HidModel model)
        {
            this.model = model;


            //現在のアクションマップを取得。
            //初期状態はPlayerInputコンポーネントのinspectorのDefaultMap
            //var actionMap = playerInput.currentActionMap;

            //アクションマップからアクションを取得
            //moveAction = actionMap["Move"];
        }

        public void Update()
        {
            //model.OnLStickHorizontalAxis(Input.GetAxis("Horizontal"));
            //model.OnLStickVerticalAxis(Input.GetAxis("Vertical"));
            //model.OnRStickHorizontalAxis(Input.GetAxis("Horizontal2"));
            //model.OnRStickVerticalAxis(Input.GetAxis("Vertical2"));

            //model.SetMove(moveAction.ReadValue<Vector2>());
        }

    }
}
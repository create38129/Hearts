using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace Assets.Script.Scene.Dungeon
{
    class CharacterPresenter : MonoBehaviour
    {
        private CharacterModel model;

        public void Initialize(CharacterModel model)
        {
            this.model = model;
        }

        public void Register()
        {
            model.Pos
                .Subscribe(pos =>
                {
                    transform.localPosition = pos;
                    Debug.LogError("pos"+pos);
                })
                .AddTo(this);
            model.Rotation
                .Subscribe(rot => 
                {
                    transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, rot);
                })
                .AddTo(this);
        }
        
    }
}

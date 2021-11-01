using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Script.Common
{
    /// <summary>
    /// 画面サイズに合わせる
    /// </summary>
    public class AspectFixedComponent : MonoBehaviour
    {
        enum FixedType
        {
            AspectFixed,
            BaseFixed,
        };


        [SerializeField]
        private Camera targetCamera = default;

        [SerializeField]
        private float baseWidth = 16.0f;
        [SerializeField]
        private float baseHeight = 9.0f;
        [SerializeField]
        private FixedType type;


        void Awake()
        {
            switch (type)
            {
                case FixedType.AspectFixed:
                    // アスペクト比固定
                    var scale = Mathf.Min(Screen.height / this.baseHeight, Screen.width / this.baseWidth);
                    var width = (this.baseWidth * scale) / Screen.width;
                    var height = (this.baseHeight * scale) / Screen.height;
                    this.targetCamera.rect = new Rect((1.0f - width) * 0.5f, (1.0f - height) * 0.5f, width, height);
                    break;
                case FixedType.BaseFixed:
                    // ベース維持
                    var scaleWidth = (Screen.height / this.baseHeight) * (this.baseWidth / Screen.width);
                    var scaleRatio = Mathf.Max(scaleWidth, 1.0f);
                    this.targetCamera.fieldOfView = Mathf.Atan(Mathf.Tan(this.targetCamera.fieldOfView * 0.5f * Mathf.Deg2Rad) * scaleRatio) * 2.0f * Mathf.Rad2Deg;
                    break;
                default:
                    break;
            }
        }

    }
}

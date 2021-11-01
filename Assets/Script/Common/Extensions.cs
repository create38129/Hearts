using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Script.Common
{
    static class Extensions
    {
        public static IDisposable Tween(Subject<float> subject, float duration)
        {
            return Observable.FromCoroutine(() => UpdateInternal(subject, duration)).Subscribe();
        }

        public static IEnumerator UpdateInternal(Subject<float> subject, float duration)
        {
            float elapsed = 0;
            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                subject.OnNext(elapsed / duration);
                yield return null;
            }
            subject.OnNext(1.0f);
            subject.OnCompleted();
        }


        public static void SetAlpha(this Graphic self, float alpha)
        {
            self.color = new Color(self.color.r, self.color.g, self.color.b, alpha);
        }
        public static void SetColor(this Graphic self, Color color)
        {
            self.color = color;
        }
    }
}

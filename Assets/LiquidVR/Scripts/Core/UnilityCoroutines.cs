using System;
using System.Collections;
using UnityEngine;

namespace Liquid.Core
{
    public static class UnilityCoroutines
    {
        public static IEnumerator PerformOverTime(Action action, float time, bool isRealtime = false)
        {
            var waitFor = new WaitForEndOfFrame();
            for (var i = 0f; i <= time; i += isRealtime ? Time.unscaledDeltaTime : Time.deltaTime)
            {
                action?.Invoke();
                yield return waitFor;
            }
        }

        public static IEnumerator LerpOverTime(Action<float> action, float time, bool isRealtime = false)
        {
            var waitFor = new WaitForEndOfFrame();
            for (var i = 0f; i <= time; i += isRealtime ? Time.unscaledDeltaTime : Time.deltaTime)
            {
                action?.Invoke(i / time);
                yield return waitFor;
            }
            action?.Invoke(1f);
        }

        public static IEnumerator PerformContinuous(Action action, float delay, bool isRealtime = false)
        {
            for (;;)
            {
                action?.Invoke();

                if (isRealtime) yield return new WaitForSecondsRealtime(delay);
                else yield return new WaitForSeconds(delay);
            }
        }
    }
}
using System.Collections;
using UnityEngine;
using Liquid.Core;

namespace Liquid.Interactables
{
    [RequireComponent(typeof(LiquidSwitcher))]
    public class LiquidSwitcherAnimator : LiquidBaseAnimator
    {
        public override void SetStartPoint()
        {
            StartPoint = this.transform.localEulerAngles;
        }

        public override void SetEndPoint()
        {
            EndPoint = this.transform.localEulerAngles;
        }

        protected override IEnumerator AnimatePress()
        {
            OnAnimationBegin?.Invoke();
            yield return AnimateTarget(this.transform.localEulerAngles, EndPoint);
        }

        protected override IEnumerator AnimateRelease()
        {
            yield return AnimateTarget(this.transform.localEulerAngles, StartPoint);
            OnAnimationEnd?.Invoke();
        }

        protected override IEnumerator AnimateTarget(Vector3 start, Vector3 end)
        {
            var waitFor = new WaitForEndOfFrame();
            for (var i = 0f; i <= m_animationTime; i += Time.deltaTime)
            {
                this.transform.localEulerAngles = Vector3.Lerp(start, end, i / m_animationTime);
                yield return waitFor;
            }
            this.transform.localEulerAngles = end;
        }
    }
}
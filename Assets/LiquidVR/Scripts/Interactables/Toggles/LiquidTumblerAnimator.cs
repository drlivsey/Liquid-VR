using System.Collections;
using UnityEngine;
using Liquid.Core;

namespace Liquid.Interactables
{
    [RequireComponent(typeof(LiquidTumbler))]
    public class LiquidTumblerAnimator : LiquidBaseAnimator
    {
        protected override void ProcessRelease()
        {
            if (_targetLiquidToggle.InteractionType == LiquidHandInteraction.Hold)
            {
                PlayReleaseAnimation();
            }
            else 
            {
                PlayTouchAnimation();
            }
        }

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
                this.transform.localEulerAngles = Extensions.LerpAngle(start, end, i / m_animationTime);
                yield return waitFor;
            }
            this.transform.localEulerAngles = end;
        }

        protected override IEnumerator AnimateTouch()
        {
            if (_targetLiquidToggle.IsPressed) 
            {
                yield return AnimateRelease();
            }
            else 
            {
                yield return AnimatePress();
            }
        }
    }
}
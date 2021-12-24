using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Liquid.Core;

namespace Liquid.Utils
{
    public class RotationProceduralAnimator : SimpleProceduralAnimator
    {
        protected override void UpdateTransform(Direction direction, float t)
        {
            this.transform.localEulerAngles = GetLerpedValue(direction, t);
        }

        protected override void SetValue(out Vector3 value)
        {
            value = this.transform.localEulerAngles;
        }

        protected virtual Vector3 GetLerpedValue(Direction direction, float t)
        {
            switch (direction)
            {
                case Direction.Forward:
                return Extensions.LerpAngle(m_beginValue, m_endValue, t);
                case Direction.Backward:
                return Extensions.LerpAngle(m_endValue, m_beginValue, t);
                default: return Vector3.zero;
            }
        }
    }
}
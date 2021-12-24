using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Liquid.Core;

namespace Liquid.Utils
{
    public class QuaternionLerper : BaseLerper
    {
        [SerializeField] private Quaternion m_beginValue = Quaternion.identity;
        [SerializeField] private Quaternion m_endValue = Quaternion.identity;
        [SerializeField] private QuaternionEvent m_onLerpTick;

        public QuaternionEvent OnLerpTick => m_onLerpTick;
        public Quaternion CurrentValue { get; private set; }

        protected override void LerpValue(float t)
        {
            CurrentValue = Quaternion.Lerp(m_beginValue, m_endValue, t);
            OnLerpTick?.Invoke(CurrentValue);
        }
    }

}

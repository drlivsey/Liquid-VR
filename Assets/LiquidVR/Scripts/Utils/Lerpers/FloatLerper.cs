using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Liquid.Core;

namespace Liquid.Utils
{
    public class FloatLerper : BaseLerper
    {
        [SerializeField] private float m_beginValue = 0f;
        [SerializeField] private float m_endValue = 1f;
        [SerializeField] private FloatEvent m_onLerpTick;

        public FloatEvent OnLerpTick => m_onLerpTick;
        public float CurrentValue { get; private set; }

        protected override void LerpValue(float t)
        {
            CurrentValue = Mathf.Lerp(m_beginValue, m_endValue, t);
            OnLerpTick?.Invoke(CurrentValue);
        }
    }
}
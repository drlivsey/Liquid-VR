using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Liquid.Core;

namespace Liquid.Utils
{
    public class Vector3Lerper : BaseLerper
    {
        [SerializeField] private Vector3 m_beginValue = Vector3.zero;
        [SerializeField] private Vector3 m_endValue = Vector3.one;
        [SerializeField] private Vector3Event m_onLerpTick;

        public Vector3Event OnLerpTick => m_onLerpTick;
        public Vector3 CurrentValue { get; private set; }

        protected override void LerpValue(float t)
        {
            CurrentValue = Vector3.Lerp(m_beginValue, m_endValue, t);
            OnLerpTick?.Invoke(CurrentValue);
        }
    }
}

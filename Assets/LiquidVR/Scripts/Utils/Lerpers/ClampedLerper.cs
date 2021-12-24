using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Liquid.Core;

namespace Liquid.Utils
{
    public class ClampedLerper : BaseLerper
    {
        [SerializeField] private FloatEvent m_onLerpTick;
        
        public FloatEvent OnLerpTick => m_onLerpTick;
        public float CurrentValue { get; private set; }

        protected override void LerpValue(float t)
        {
            CurrentValue = Mathf.Lerp(0f, 1f, t);
            OnLerpTick?.Invoke(CurrentValue);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Liquid.Core;

namespace Liquid.Utils
{
    public class ColorLerper : BaseLerper
    {
        [SerializeField] private Color m_beginValue = Color.black;
        [SerializeField] private Color m_endValue = Color.white;
        [SerializeField] private ColorEvent m_onLerpTick;

        public ColorEvent OnLerpTick => m_onLerpTick;
        public Color CurrentValue { get; private set; }

        protected override void LerpValue(float t)
        {
            CurrentValue = Color.Lerp(m_beginValue, m_endValue, t);
            OnLerpTick?.Invoke(CurrentValue);
        }
    }
}
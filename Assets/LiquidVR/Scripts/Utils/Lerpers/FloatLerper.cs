using UnityEngine;
using Liquid.Core;

namespace Liquid.Utils.Lerpers
{
    public class FloatLerper : BaseLerper
    {
        [SerializeField] private float m_beginValue = 0f;
        [SerializeField] private float m_endValue = 1f;
        [SerializeField] private FloatEvent m_onLerpTick = new FloatEvent();

        public FloatEvent OnLerpTick => m_onLerpTick;

        public float BeginValue
        {
            get => m_beginValue;
            set => m_beginValue = value;
        }

        public float EndValue
        {
            get => m_endValue;
            set => m_endValue = value;
        }

        public float CurrentValue { get; private set; }

        protected override void LerpValue(float t)
        {
            CurrentValue = Mathf.Lerp(m_beginValue, m_endValue, t);
            OnLerpTick?.Invoke(CurrentValue);
        }
    }
}
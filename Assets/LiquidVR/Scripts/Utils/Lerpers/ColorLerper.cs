using UnityEngine;
using Liquid.Core;

namespace Liquid.Utils.Lerpers
{
    public class ColorLerper : BaseLerper
    {
        [SerializeField] private Color m_beginValue = Color.black;
        [SerializeField] private Color m_endValue = Color.white;
        [SerializeField] private ColorEvent m_onLerpTick = new ColorEvent();

        public ColorEvent OnLerpTick => m_onLerpTick;

        public Color BeginValue
        {
            get => m_beginValue;
            set => m_beginValue = value;
        }

        public Color EndValue
        {
            get => m_endValue;
            set => m_endValue = value;
        }

        public Color CurrentValue { get; private set; }

        protected override void LerpValue(float t)
        {
            CurrentValue = Color.Lerp(m_beginValue, m_endValue, t);
            OnLerpTick?.Invoke(CurrentValue);
        }
    }
}
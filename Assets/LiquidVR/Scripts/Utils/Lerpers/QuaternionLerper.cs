using UnityEngine;
using Liquid.Core;

namespace Liquid.Utils.Lerpers
{
    public class QuaternionLerper : BaseLerper
    {
        [SerializeField] private Quaternion m_beginValue = Quaternion.identity;
        [SerializeField] private Quaternion m_endValue = Quaternion.identity;
        [SerializeField] private QuaternionEvent m_onLerpTick = new QuaternionEvent();

        public QuaternionEvent OnLerpTick => m_onLerpTick;

        public Quaternion BeginValue
        {
            get => m_beginValue;
            set => m_beginValue = value;
        }

        public Quaternion EndValue
        {
            get => m_endValue;
            set => m_endValue = value;
        }

        public Quaternion CurrentValue { get; private set; }

        protected override void LerpValue(float t)
        {
            CurrentValue = Quaternion.Lerp(m_beginValue, m_endValue, t);
            OnLerpTick?.Invoke(CurrentValue);
        }
    }

}

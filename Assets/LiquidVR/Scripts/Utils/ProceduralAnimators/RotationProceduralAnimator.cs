using UnityEngine;
using Liquid.Core;

namespace Liquid.Utils
{
    public class RotationProceduralAnimator : SimpleProceduralAnimator
    {
        [SerializeField] protected Vector3 m_beginValue = Vector3.zero;
        [SerializeField] protected Vector3 m_endValue = Vector3.one;

        private Transform _targetTransform = null;

        public override void SetBeginValues()
        {
            m_beginValue = this.transform.localEulerAngles;
        }

        public override void SetEndValues()
        {
            m_endValue = this.transform.localEulerAngles;
        }

        public void SetBeginValues(Vector3 value)
        {
            m_beginValue = value;
        }

        public void SetEndValues(Vector3 value)
        {
            m_endValue = value;
        }

        protected override void InitializeComponent()
        {
            _targetTransform = this.transform;
        }

        protected override void UpdateAnimation(Direction direction, float t)
        {
            this.transform.localEulerAngles = GetLerpedValue(direction, t);
        }

        protected virtual Vector3 GetLerpedValue(Direction direction, float t)
        {
            switch (direction)
            {
                case Direction.Forward:
                return Vector3.Lerp(m_beginValue, m_endValue, t);
                case Direction.Backward:
                return Vector3.Lerp(m_endValue, m_beginValue, t);
                default: return Vector3.zero;
            }
        }
    }
}
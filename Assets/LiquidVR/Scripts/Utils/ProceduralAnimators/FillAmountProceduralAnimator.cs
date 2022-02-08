using UnityEngine;
using UnityEngine.UI;
using Liquid.Core;

namespace Liquid.Utils
{
    [RequireComponent(typeof(Image))]
    public class FillAmountProceduralAnimator : SimpleProceduralAnimator
    {
        [SerializeField] private float m_beginValue = 0f;
        [SerializeField] private float m_endValue = 1f;

        private Image _targetImage = null;

        public override void SetBeginValues()
        {
            m_beginValue = GetComponent<Image>().fillAmount;
        }

        public override void SetEndValues()
        {
            m_endValue = GetComponent<Image>().fillAmount;
        }

        public void SetBeginValues(float fill)
        {
            m_beginValue = fill;
        }

        public void SetEndValues(float fill)
        {
            m_endValue = fill;
        }

        protected override void UpdateAnimation(Direction direction, float t)
        {
            _targetImage.fillAmount = GetLerpedValue(direction, t);
        }

        protected override void InitializeComponent()
        {
            _targetImage = GetComponent<Image>();
        }

        protected virtual float GetLerpedValue(Direction direction, float t)
        {
            switch (direction)
            {
                case Direction.Forward:
                return Mathf.Lerp(m_beginValue, m_endValue, t);
                case Direction.Backward:
                return Mathf.Lerp(m_endValue, m_beginValue, t);
                default: return 1f;
            }
        }
    }
}
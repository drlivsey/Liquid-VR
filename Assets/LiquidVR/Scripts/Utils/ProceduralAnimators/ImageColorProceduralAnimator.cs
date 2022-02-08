using UnityEngine;
using UnityEngine.UI;
using Liquid.Core;

namespace Liquid.Utils
{
    [RequireComponent(typeof(Image))]
    public class ImageColorProceduralAnimator : SimpleProceduralAnimator
    {
        [SerializeField] private Color m_beginValue = Color.black;
        [SerializeField] private Color m_endValue = Color.black;

        private Image _targetImage = null;

        public override void SetBeginValues()
        {
            m_beginValue = GetComponent<Image>().color;
        }

        public override void SetEndValues()
        {
            m_endValue = GetComponent<Image>().color;
        }

        public void SetBeginValues(Color color)
        {
            m_beginValue = color;
        }

        public void SetEndValues(Color color)
        {
            m_endValue = color;
        }

        protected override void UpdateAnimation(Direction direction, float t)
        {
            _targetImage.color = GetLerpedValue(direction, t);
        }

        protected override void InitializeComponent()
        {
            _targetImage = GetComponent<Image>();
        }

        protected virtual Color GetLerpedValue(Direction direction, float t)
        {
            switch (direction)
            {
                case Direction.Forward:
                return Color.Lerp(m_beginValue, m_endValue, t);
                case Direction.Backward:
                return Color.Lerp(m_endValue, m_beginValue, t);
                default: return Color.white;
            }
        }
    }
}
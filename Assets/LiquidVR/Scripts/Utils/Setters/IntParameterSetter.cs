using UnityEngine;

namespace Liquid.Utils.Setters
{
    public class IntParameterSetter : SimpleParameterSetter
    {
        [SerializeField] private int m_parameterValue = 0;

        private int _defaultParameterValue = 0;

        public override void SetParameter()
        {
            SetParameter(m_parameterValue);
        }

        public void SetParameter(int value)
        {
            _targetMaterial.SetInt(ParameterName, value);
        }

        public override void RestoreParameter()
        {
            _targetMaterial.SetInt(ParameterName, _defaultParameterValue);
        }

        protected override void InitializeComponent()
        {
            base.InitializeComponent();
            _defaultParameterValue = _targetMaterial.GetInt(ParameterName);
        }
    }
}
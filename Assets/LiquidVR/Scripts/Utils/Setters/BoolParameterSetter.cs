using UnityEngine;

namespace Liquid.Utils.Setters
{
    public class BoolParameterSetter : SimpleParameterSetter
    {
        [SerializeField] private bool m_parameterValue = false;

        private int _defaultParameterValue = 0;

        public override void SetParameter()
        {
            SetParameter(m_parameterValue);
        }

        public void SetParameter(bool value)
        {
            _targetMaterial.SetInt(ParameterName, value ? 1 : 0);
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
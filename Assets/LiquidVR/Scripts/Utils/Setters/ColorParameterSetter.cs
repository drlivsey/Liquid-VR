using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Liquid.Utils
{
    public class ColorParameterSetter : SimpleParameterSetter
    {
        [SerializeField] private Color m_parameterValue = Color.white;

        private Color _defaultParameterValue = Color.white;

        public override void SetParameter()
        {
            SetParameter(m_parameterValue);
        }

        public void SetParameter(Color value)
        {
            _targetMaterial.SetColor(ParameterName, value);
        }

        public override void RestoreParameter()
        {
            _targetMaterial.SetColor(ParameterName, _defaultParameterValue);
        }

        protected override void InitializeComponent()
        {
            base.InitializeComponent();
            _defaultParameterValue = _targetMaterial.GetColor(ParameterName);
        }
    }
}
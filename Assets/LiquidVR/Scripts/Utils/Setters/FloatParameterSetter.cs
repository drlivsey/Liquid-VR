using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Liquid.Utils.Setters
{
    public class FloatParameterSetter : SimpleParameterSetter
    {
        [SerializeField] private float m_parameterValue = 0f;

        private float _defaultParameterValue = 0f;

        public override void SetParameter()
        {
            SetParameter(m_parameterValue);
        }

        public void SetParameter(float value)
        {
            _targetMaterial.SetFloat(ParameterName, value);
        }

        public override void RestoreParameter()
        {
            _targetMaterial.SetFloat(ParameterName, _defaultParameterValue);
        }

        protected override void InitializeComponent()
        {
            base.InitializeComponent();
            _defaultParameterValue = _targetMaterial.GetFloat(ParameterName);
        }
    }
}
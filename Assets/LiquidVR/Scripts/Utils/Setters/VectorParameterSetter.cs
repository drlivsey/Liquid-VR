using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Liquid.Utils
{
    public class VectorParameterSetter : SimpleParameterSetter
    {
        [SerializeField] private Vector4 m_parameterValue = Vector4.zero;

        private Vector4 _defaultParameterValue = Vector4.zero;

        public override void SetParameter()
        {
            SetParameter(m_parameterValue);
        }

        public void SetParameter(Vector4 value)
        {
            _targetMaterial.SetVector(ParameterName, value);
        }

        public override void RestoreParameter()
        {
            _targetMaterial.SetVector(ParameterName, _defaultParameterValue);
        }

        protected override void InitializeComponent()
        {
            base.InitializeComponent();
            _defaultParameterValue = _targetMaterial.GetVector(ParameterName);
        }
    }
}
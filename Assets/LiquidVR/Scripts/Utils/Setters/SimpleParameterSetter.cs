using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Liquid.Utils.Setters
{
    [RequireComponent(typeof(Renderer))]
    public abstract class SimpleParameterSetter : BaseParameterSetter
    {
        [SerializeField] private string m_parameterName = string.Empty;
        [SerializeField] private bool m_useSharedMaterial = false;
        [SerializeField] private bool m_resetOnDisable = false;

        public string ParameterName
        {
            get => m_parameterName;
            private set => m_parameterName = value;
        }

        protected Material _targetMaterial = null;
        protected Renderer _targetRenderer = null;

        protected virtual void Awake() 
        {
            InitializeComponent();
        }

        protected virtual void OnDisable()
        {
            if (m_resetOnDisable) RestoreParameter();
        }

        protected virtual void InitializeComponent()
        {
            _targetRenderer = GetComponent<Renderer>();
            _targetMaterial = m_useSharedMaterial ? _targetRenderer.sharedMaterial : _targetRenderer.material;
        }
    }
}
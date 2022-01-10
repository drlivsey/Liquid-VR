using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Liquid.Core;

namespace Liquid.Subsystems.Updating
{
    public abstract class BaseUpdatableComponent : MonoBehaviour
    {
        [SerializeField] private UpdatingState m_updatingState = UpdatingState.Update;
        
        [SerializeField] private float m_updateFrequency = 0f;
        [SerializeField] private bool m_updateInRealtime = false;
        
        [SerializeField] private UnityEvent m_onUpdate = new UnityEvent();
        [SerializeField] private UnityEvent m_onRegister = new UnityEvent();
        [SerializeField] private UnityEvent m_onUnregister = new UnityEvent();

        public float UpdateFrequency
        {
            get => m_updateFrequency;
            set => m_updateFrequency = value;
        }

        public bool UpdateInRealtime
        {
            get => m_updateInRealtime;
            set => m_updateInRealtime = value;
        }

        public UpdatingState UpdatingState
        {
            get => m_updatingState;
            set => m_updatingState = value;
        }

        public UnityEvent OnUpdate => m_onUpdate;
        public UnityEvent OnRegister => m_onRegister;
        public UnityEvent OnUnregister => m_onUnregister;

        private UpdatingSubsystem _updatingSubsystem = null;

        private void Awake()
        {
            if (_updatingSubsystem == null)
            {
                _updatingSubsystem = UpdatingSubsystem.SubsystemInstance();
            }
        }

        private void OnEnable()
        {
            RegisterComponent();
        }

        private void OnDisable()
        {
            UnregisterComponent();
        }

        public void UpdateComponent()
        {
            OnUpdate?.Invoke();
        }

        private void RegisterComponent()
        {
            _updatingSubsystem.RegisterComponent(this);
            OnRegister?.Invoke();
        }

        private void UnregisterComponent()
        {
            _updatingSubsystem.UnregisterComponent(this);
            OnUnregister?.Invoke();
        }
    }
}
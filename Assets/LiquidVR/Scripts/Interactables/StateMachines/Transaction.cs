using System;
using UnityEngine;

namespace Liquid.StateMachines
{
    [Serializable]
    public struct Transaction
    {
        [SerializeField] private SimpleState m_initialState;
        [SerializeField] private SimpleState m_targetState;
        [SerializeField, Min(0)] private float m_duration;

        public SimpleState InitialState => m_initialState;
        public SimpleState TargetState => m_targetState;
        public float Duration => m_duration;
    }
}
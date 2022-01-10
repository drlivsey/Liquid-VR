using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace Liquid.Utils
{
    public class ConditionsList : MonoBehaviour
    {
        [SerializeField] private Condition[] m_conditions = null;
        [SerializeField, Header("Events")] private UnityEvent m_onAllConditionsFulfilled = new UnityEvent();
        [SerializeField] private UnityEvent m_onAnyConditionUnfulfilled = new UnityEvent();

        public Condition[] Conditions
        {
            get => m_conditions;
            set => m_conditions = value;
        }

        public UnityEvent OnAllConditionsFulfilled
        {
            get => m_onAllConditionsFulfilled;
            set => m_onAllConditionsFulfilled = value;
        }

        public UnityEvent OnAnyConditionUnfulfilled
        {
            get => m_onAnyConditionUnfulfilled;
            set => m_onAnyConditionUnfulfilled = value;
        }

        public bool IsAllMet
        {
            get => _isAllMet;
            set => _isAllMet = value;
        }

        private bool _isAllMet = false;

        public void MarkAsFulfilledByIndex(int index)
        {
            if (index < 0 || index >= m_conditions.Length) return;
            
            m_conditions[index].MarkAsFulfilled();
            ValidateConditions();
        }
        
        public void MarkAsUnfulfilledByIndex(int index)
        {
            if (index < 0 || index >= m_conditions.Length) return;
            
            m_conditions[index].MarkAsUnfulfilled();
            ValidateConditions();
        }
        
        public void MarkAsFulfilledByName(string conditionName)
        {
            if (TryGetConditionByName(conditionName, out var condition))
            {
                condition.MarkAsFulfilled();
                ValidateConditions();
            }
        }
        
        public void MarkAsUnfulfilledByName(string conditionName)
        {
            if (TryGetConditionByName(conditionName, out var condition))
            {
                condition.MarkAsUnfulfilled();
                ValidateConditions();
            }
        }

        private bool TryGetConditionByName(string conditionName, out Condition condition)
        {
            foreach (var child in m_conditions)
            {
                if (child.Name.Equals(conditionName) == false) continue;
                
                condition = child;
                return true;
            }

            condition = null;
            return false;
        }

        private void ValidateConditions()
        {
            if (m_conditions.Any(condition => condition.IsMet == false))
            {
                if (_isAllMet == false) return;

                _isAllMet = false;
                m_onAnyConditionUnfulfilled?.Invoke();
                return;
            }

            _isAllMet = true;
            m_onAllConditionsFulfilled?.Invoke();
        }

        [Serializable]
        public class Condition
        {
            [SerializeField] private string m_name = "Condition";
            [SerializeField] private bool m_isMet = false;
            [SerializeField] private UnityEvent m_onFulfilled  = new UnityEvent();
            [SerializeField] private UnityEvent m_onUnfulfilled = new UnityEvent();

            public string Name => m_name;

            public bool IsMet
            {
                get => m_isMet;
                private set => m_isMet = value;
            }

            public UnityEvent OnFulfilled
            {
                get => m_onFulfilled;
                set => m_onFulfilled = value;
            }

            public UnityEvent OnUnfulfilled
            {
                get => m_onUnfulfilled;
                set => m_onUnfulfilled = value;
            }

            public void MarkAsFulfilled()
            {
                m_isMet = true;
                m_onFulfilled?.Invoke();
            }

            public void MarkAsUnfulfilled()
            {
                m_isMet = false;
                m_onUnfulfilled?.Invoke();
            }
        }
    }
}
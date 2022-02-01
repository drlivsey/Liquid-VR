using System.Collections;
using UnityEngine;

namespace Liquid.StateMachines
{
    public class SimpleStateMachine : MonoBehaviour, IStateMachine
    {
        [SerializeField] private SimpleState m_initialState = null;
        [SerializeField] private Transaction[] m_transactions = null;

        public Transaction[] Transactions => m_transactions;

        private SimpleState _currentState = null;
        private bool _isCurrentlyTransacting = false;

        private void Awake()
        {
            if (m_initialState)
            {
                _currentState = m_initialState;
            }
            else if (m_transactions?.Length > 0)
            {
                _currentState = m_transactions?[0].InitialState;
            }
        }

        public bool TryMakeTransaction(IState state)
        {
            if (CanMakeTransaction(state))
            {
                MakeTransaction(state);
                return true;
            }
            return false;
        }

        public bool TryMakeTransaction(string stateName)
        {
            if (CanMakeTransaction(stateName))
            {
                MakeTransaction(stateName);
                return true;
            }
            return false;
        }

        public void MakeTransaction(IState state)
        {
            TryFindTransaction(state, out var transaction);
            StartCoroutine(WaitForTransaction(transaction));
        }

        public void MakeTransaction(string stateName)
        {
            TryFindTransactionByStateName(stateName, out var transaction);
            StartCoroutine(WaitForTransaction(transaction));
        }

        public bool CanMakeTransaction(IState state)
        {
            return TryFindTransaction(state, out var transaction) && !_isCurrentlyTransacting;
        }

        public bool CanMakeTransaction(string stateName)
        {
            return TryFindTransactionByStateName(stateName, out var transaction) && !_isCurrentlyTransacting;
        }

        public bool IsCurrentState(IState state)
        {
            return _currentState.Equals(state);
        }

        public bool IsCurrentState(string stateName)
        {
            return _currentState.name.Equals(stateName);
        }

        protected IEnumerator WaitForTransaction(Transaction transaction)
        {
            _isCurrentlyTransacting = true;
            transaction.InitialState.Exit();
            
            yield return new WaitForSecondsRealtime(transaction.Duration);
            
            transaction.TargetState.Enter();
            _currentState = transaction.TargetState;
            _isCurrentlyTransacting = false;
        }

        private bool TryFindTransaction(IState state, out Transaction transaction)
        {
            transaction = default(Transaction);
            foreach (var element in m_transactions)
            {
                if (element.InitialState.Equals(_currentState) && element.TargetState.Equals(state))
                {
                    transaction = element;
                    return true;
                }
            }

            return false;
        }

        private bool TryFindTransactionByStateName(string stateName, out Transaction transaction)
        {
            transaction = default(Transaction);
            foreach (var element in m_transactions)
            {
                if (element.InitialState.Equals(_currentState) && element.TargetState.name.Equals(stateName))
                {
                    transaction = element;
                    return true;
                }
            }

            return false;
        }
    }
}
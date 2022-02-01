using UnityEngine;
using UnityEngine.Events;

namespace Liquid.StateMachines
{
    [CreateAssetMenu(fileName = "SimpleState", menuName = "Liquid VR/State Machine/SimpleState", order = 1)]
    public class SimpleState : ScriptableObject, IState
    {
        public event UnityAction OnEnter;
        public event UnityAction OnExit;
        
        public void Enter()
        {
            OnEnter?.Invoke();
        }

        public void Exit()
        {
            OnExit?.Invoke();
        }
    }
}

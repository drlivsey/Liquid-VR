using UnityEngine;
using UnityEngine.Events;

namespace Liquid.Utils
{
    public class Semaphore : MonoBehaviour
    {
        [SerializeField, Header("Events")] private UnityEvent m_onEnable = new UnityEvent();
        [SerializeField] private UnityEvent m_onDisable = new UnityEvent();
        [SerializeField] private UnityEvent m_onStateChange = new UnityEvent();

        public UnityEvent OnEnable => m_onEnable;
        public UnityEvent OnDisable => m_onDisable;
        public UnityEvent OnStateChange => m_onStateChange;

        private bool _isActive = false;

        public void SwitchState()
        {
            if (_isActive)
            {
                OnDisable?.Invoke();
                _isActive = false;
            }
            else
            {
                OnEnable?.Invoke();
                _isActive = true;
            }
            OnStateChange?.Invoke();
        }
    }
}
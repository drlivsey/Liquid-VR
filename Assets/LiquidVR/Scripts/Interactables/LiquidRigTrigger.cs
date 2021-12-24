using UnityEngine;
using UnityEngine.Events;

namespace Liquid.Interactables
{
    [RequireComponent(typeof(Collider))]
    public class LiquidRigTrigger : MonoBehaviour
    {
        [SerializeField] private UnityEvent m_onSelectEntering;
        [SerializeField] private UnityEvent m_onSelectExiting;

        public UnityEvent OnSelectEntering => m_onSelectEntering;
        public UnityEvent OnSelectExiting => m_onSelectExiting;

        private void OnTriggerEnter(Collider other) 
        {
            if (other.TryGetComponent<LiquidRigCamera>(out var playerCamera))
            {
                OnSelectEntering?.Invoke();
            }
        }
        private void OnTriggerExit(Collider other) 
        {
            if (other.TryGetComponent<LiquidRigCamera>(out var playerCamera))
            {
                OnSelectExiting?.Invoke();
            }
        }
    }
}
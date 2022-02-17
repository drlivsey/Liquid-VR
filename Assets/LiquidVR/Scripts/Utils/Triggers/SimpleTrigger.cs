using UnityEngine;
using UnityEngine.Events;

namespace Liquid.Utils
{
    [RequireComponent(typeof(Collider))]
    public class SimpleTrigger : MonoBehaviour
    {
        [SerializeField] private UnityEvent m_onTriggerEntering = new UnityEvent();
        [SerializeField] private UnityEvent m_onTriggerExiting = new UnityEvent();
        [SerializeField] private UnityEvent m_onTriggerStaying = new UnityEvent();

        public UnityEvent OnTriggerEntering
        {
            get => m_onTriggerEntering;
            set => m_onTriggerEntering = value;
        }

        public UnityEvent OnTriggerExiting
        {
            get => m_onTriggerExiting;
            set => m_onTriggerExiting = value;
        }

        public UnityEvent OnTriggerStaying
        {
            get => m_onTriggerStaying;
            set => m_onTriggerStaying = value;
        }

        private void Awake()
        {
            GetComponent<Collider>().isTrigger = true;
        }

        protected virtual void OnTriggerEnter(Collider other)
        {
            m_onTriggerEntering?.Invoke();
        }
        
        protected virtual void OnTriggerExit(Collider other)
        {
            m_onTriggerExiting?.Invoke();
        }
        
        protected virtual void OnTriggerStay(Collider other)
        {
            m_onTriggerStaying?.Invoke();
        }
    }
}
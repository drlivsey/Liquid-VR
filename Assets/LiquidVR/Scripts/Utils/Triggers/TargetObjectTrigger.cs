using UnityEngine;

namespace Liquid.Utils
{
    public class TargetObjectTrigger : SimpleTrigger
    {
        [SerializeField] private GameObject m_targetGameObject = null;

        public GameObject TargetGameObject
        {
            get => m_targetGameObject;
            set => m_targetGameObject = value;
        }

        protected override void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.Equals(m_targetGameObject))
            {
                base.OnTriggerEnter(other);
            }
        }

        protected override void OnTriggerExit(Collider other)
        {
            if (other.gameObject.Equals(m_targetGameObject))
            {
                base.OnTriggerExit(other);
            }
        }

        protected override void OnTriggerStay(Collider other)
        {
            if (other.gameObject.Equals(m_targetGameObject))
            {
                base.OnTriggerStay(other);
            }
        }
    }
}
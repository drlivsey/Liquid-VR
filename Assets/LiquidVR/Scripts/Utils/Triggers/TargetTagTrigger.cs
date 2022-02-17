using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Liquid.Core;

namespace Liquid.Utils
{
    public class TargetTagTrigger : SimpleTrigger
    {
        [SerializeField, TagSelector] private string m_targetTag = String.Empty;

        public string TargetTag
        {
            get => m_targetTag;
            set => m_targetTag = value;
        }

        protected override void OnTriggerEnter(Collider other)
        {
            if (other.tag.Equals(m_targetTag))
            {
                base.OnTriggerEnter(other);
            }
        }

        protected override void OnTriggerExit(Collider other)
        {
            if (other.tag.Equals(m_targetTag))
            {
                base.OnTriggerExit(other);
            }
        }

        protected override void OnTriggerStay(Collider other)
        {
            if (other.tag.Equals(m_targetTag))
            {
                base.OnTriggerStay(other);
            }
        }
    }
}

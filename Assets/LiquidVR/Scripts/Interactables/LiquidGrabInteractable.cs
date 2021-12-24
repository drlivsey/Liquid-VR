using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Liquid.Core;

namespace Liquid.Interactables
{
    public class LiquidGrabInteractable : XRGrabInteractable
    {
        [SerializeField] private bool m_animateHands = false;
        [SerializeField] private string m_grabStateName = string.Empty;
        [SerializeField] private string m_releaseStateName = string.Empty;

        public bool IsAnimateHands
        {
            get => m_animateHands;
            protected set => m_animateHands = value;
        }

        public string GrabStateName
        {
            get => m_grabStateName;
            protected set => m_grabStateName = value;
        }

        public string ReleaseStateName
        {
            get => m_releaseStateName;
            protected set => m_releaseStateName = value;
        }

        protected override void OnSelectEntering(SelectEnterEventArgs args)
        {
            base.OnSelectEntering(args);
        }

        protected override void OnSelectExiting(SelectExitEventArgs args)
        {
            base.OnSelectExiting(args);
        }
    }
}

using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Liquid.Core;

namespace Liquid.Interactables
{
    [AddComponentMenu("")]
    public class LiquidToggleHoverInteractable : XRSimpleInteractable
    {
        [SerializeField] protected LiquidBaseToggle m_targetToggle = null;
        [SerializeField] private bool m_animateHand = false;
        [SerializeField] private string m_toggleStateName = string.Empty;
        [SerializeField] private string m_toggleReleaseStateName = string.Empty;

        protected override void OnEnable() 
        {
            base.OnEnable();
            lastHoverExited?.AddListener(DisableInteractions);
            firstHoverEntered?.AddListener(EnableInteractions);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            lastHoverExited?.RemoveListener(DisableInteractions);
            firstHoverEntered?.RemoveListener(EnableInteractions);
        }

        protected override void OnHoverEntered(HoverEnterEventArgs args)
        {
            base.OnHoverEntered(args);
            ProcessHoverEnter(args.interactor);
        }

        protected override void OnHoverExited(HoverExitEventArgs args)
        {
            base.OnHoverExited(args);
            ProcessHoverExit(args.interactor);
        }

        private void ProcessHoverEnter(XRBaseInteractor interactor)
        {
            if (interactor == LiquidPlayer.LeftGrabInteractor)
            {
                SetControllerState(ControllerType.LeftController, ControllerState.Interacts);
                if (m_animateHand) AnimateHand(LiquidPlayer.LeftHandAnimator, m_toggleStateName);
            }
            if (interactor == LiquidPlayer.RightGrabInteractor)
            {
                SetControllerState(ControllerType.LeftController, ControllerState.Interacts);
                if (m_animateHand) AnimateHand(LiquidPlayer.LeftHandAnimator, m_toggleStateName);
            }
        }

        private void ProcessHoverExit(XRBaseInteractor interactor)
        {
            if (interactor == LiquidPlayer.LeftGrabInteractor)
            {
                RemoveControllerState(ControllerType.LeftController, ControllerState.Interacts);
                if (m_animateHand) AnimateHand(LiquidPlayer.LeftHandAnimator, m_toggleReleaseStateName);
            }
            if (interactor == LiquidPlayer.RightGrabInteractor)
            {
                RemoveControllerState(ControllerType.LeftController, ControllerState.Interacts);
                if (m_animateHand) AnimateHand(LiquidPlayer.LeftHandAnimator, m_toggleReleaseStateName);
            }
        }

        private void DisableInteractions(HoverExitEventArgs args)
        {
            if (m_targetToggle) m_targetToggle.DisableInteractions();
        }

        private void EnableInteractions(HoverEnterEventArgs args)
        {
            if (m_targetToggle) m_targetToggle.EnableInteractions();
        }

        private void SetControllerState(ControllerType type, ControllerState state)
        {
            LiquidPlayerStateMachine.AddControllerState(type, state);
        }

        private void RemoveControllerState(ControllerType type, ControllerState state)
        {
            LiquidPlayerStateMachine.RemoveControllerState(type, state);
        }

        private void AnimateHand(Animator animator, string stateName)
        {
            animator.Play(stateName, 0);
        }
    }
}
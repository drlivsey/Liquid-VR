using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Liquid.Core;

namespace Liquid.Interactables
{
    public class LiquidSwitcherInteractable : XRSimpleInteractable
    {
        [SerializeField] private LiquidSwitcher m_targetSwitcher;
        
        [SerializeField] private bool m_animateHand = false;
        [SerializeField] private string m_switcherStateName = string.Empty;
        [SerializeField] private string m_switcherReleaseStateName = string.Empty;

        public override bool IsSelectableBy(XRBaseInteractor interactor)
        {
            if (m_targetSwitcher && m_targetSwitcher.IsPressed) 
            {
                return interactor == selectingInteractor;
            }
            return base.IsSelectableBy(interactor);
        }

        protected override void OnSelectEntered(SelectEnterEventArgs args)
        {
            base.OnSelectEntered(args);
            ProcessSelectEnter(args.interactor);
        }

        protected override void OnSelectExited(SelectExitEventArgs args)
        {
            base.OnSelectExited(args);
            ProcessSelectExit(args.interactor);
        }

        private void ProcessSelectEnter(XRBaseInteractor interactor)
        {
            if (interactor == LiquidPlayer.LeftGrabInteractor)
            {
                SetControllerState(ControllerType.LeftController, ControllerState.Interacts);
                if (m_animateHand) AnimateHand(LiquidPlayer.LeftHandAnimator, m_switcherStateName);
            }
            if (interactor == LiquidPlayer.RightGrabInteractor)
            {
                SetControllerState(ControllerType.LeftController, ControllerState.Interacts);
                if (m_animateHand) AnimateHand(LiquidPlayer.LeftHandAnimator, m_switcherStateName);
            }
            if (m_targetSwitcher) m_targetSwitcher.IsPressed = true;
        }

        private void ProcessSelectExit(XRBaseInteractor interactor)
        {
            if (interactor == LiquidPlayer.LeftGrabInteractor)
            {
                RemoveControllerState(ControllerType.LeftController, ControllerState.Interacts);
                if (m_animateHand) AnimateHand(LiquidPlayer.LeftHandAnimator, m_switcherReleaseStateName);
            }
            if (interactor == LiquidPlayer.RightGrabInteractor)
            {
                RemoveControllerState(ControllerType.LeftController, ControllerState.Interacts);
                if (m_animateHand) AnimateHand(LiquidPlayer.LeftHandAnimator, m_switcherReleaseStateName);
            }
            if (m_targetSwitcher) m_targetSwitcher.IsPressed = false;
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
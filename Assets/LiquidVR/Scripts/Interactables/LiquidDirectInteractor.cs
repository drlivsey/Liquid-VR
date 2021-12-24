using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Liquid.Core;

namespace Liquid.Interactables
{
    public class LiquidDirectInteractor : XRDirectInteractor
    {
        [SerializeField] private ControllerType m_controllerType = ControllerType.Undefided;
        [SerializeField] private bool m_animateHands = false;

        public bool IsAnimateHands 
        { 
            get => m_animateHands; 
            set => m_animateHands = value; 
        }

        private XRController _targetController = null;
        private Animator _targetAnimator = null;

        protected override void Start()
        {
            base.Start();
            InitializeComponent();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            LiquidPlayer.OnHandsPairChanged += UpdateAnimator;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            LiquidPlayer.OnHandsPairChanged -= UpdateAnimator;
        }

        public override bool CanHover(XRBaseInteractable interactable)
        {
            return (LiquidPlayerStateMachine.CanTransact(ControllerState.HoldAnItem, m_controllerType) && base.CanHover(interactable));
        }

        public override bool CanSelect(XRBaseInteractable interactable)
        {
            return (LiquidPlayerStateMachine.CanTransact(ControllerState.HoldAnItem, m_controllerType) && base.CanSelect(interactable));
        }

        protected override void OnSelectEntering(SelectEnterEventArgs args)
        {
            base.OnSelectEntering(args);
            LiquidPlayerStateMachine.AddControllerState(m_controllerType, ControllerState.HoldAnItem);
            if (this.IsAnimateHands && CanBeAnimated(args.interactable))
            {
                PlayAnimation((args.interactable as LiquidGrabInteractable).GrabStateName);
            }
        }

        protected override void OnSelectExiting(SelectExitEventArgs args)
        {
            base.OnSelectExiting(args);
            LiquidPlayerStateMachine.RemoveControllerState(m_controllerType, ControllerState.HoldAnItem);
            if (this.IsAnimateHands && CanBeAnimated(args.interactable))
            {
                PlayAnimation((args.interactable as LiquidGrabInteractable).ReleaseStateName);
            }
        }
        
        protected void InitializeComponent()
        {
            _targetController = GetComponent<XRController>();
            _targetAnimator = LiquidPlayer.GetHandAnimator(m_controllerType);
        }

        private void PlayAnimation(string state)
        {
            _targetAnimator.speed = 1f;
            _targetAnimator.Play(state, 0);
        }

        private void UpdateAnimator()
        {
            _targetAnimator = LiquidPlayer.GetHandAnimator(m_controllerType);
        }

        private bool CanBeAnimated(XRBaseInteractable interactable)
        {
            if (interactable is LiquidGrabInteractable == false)
            {
                return false;
            }
            return (interactable as LiquidGrabInteractable).IsAnimateHands;
        }
    }
}
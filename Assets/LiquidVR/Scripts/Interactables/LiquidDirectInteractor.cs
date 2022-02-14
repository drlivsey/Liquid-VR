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
            if (IsLiquidInteractable(interactable))
            {
                var liquidInteractable = interactable as ILiquidInteractable;
                return (LiquidPlayerStateMachine.CanTransact(liquidInteractable.InteractionState, m_controllerType) && base.CanHover(interactable));
            }
            else return base.CanHover(interactable);
        }

        public override bool CanSelect(XRBaseInteractable interactable)
        {
            if (IsLiquidInteractable(interactable))
            {
                var liquidInteractable = interactable as ILiquidInteractable;
                return (LiquidPlayerStateMachine.CanTransact(liquidInteractable.InteractionState, m_controllerType) && base.CanSelect(interactable));
            }
            else return base.CanSelect(interactable);
        }

        protected override void OnHoverEntering(HoverEnterEventArgs args)
        {
            base.OnHoverEntering(args);
            if (IsLiquidInteractable(args.interactable))
            {
                var liquidInteractable = args.interactable as ILiquidInteractable;

                if (liquidInteractable.InteractionAction.Equals(InteractionTriggerAction.Hover))
                {
                    LiquidPlayerStateMachine.AddControllerState(m_controllerType, liquidInteractable.InteractionState);
                }

                if (this.IsAnimateHands && liquidInteractable.AnimationSettings.AnimateOnHover)
                {
                    PlayAnimation(liquidInteractable.AnimationSettings.HoverEnterState);
                }
            }
        }

        protected override void OnHoverExiting(HoverExitEventArgs args)
        {
            base.OnHoverExiting(args);
            if (IsLiquidInteractable(args.interactable))
            {
                var liquidInteractable = args.interactable as ILiquidInteractable;

                if (liquidInteractable.InteractionAction.Equals(InteractionTriggerAction.Hover))
                {
                    LiquidPlayerStateMachine.RemoveControllerState(m_controllerType, liquidInteractable.InteractionState);
                }

                if (this.IsAnimateHands && liquidInteractable.AnimationSettings.AnimateOnHover)
                {
                    PlayAnimation(liquidInteractable.AnimationSettings.HoverExitState);
                }
            }
        }

        protected override void OnSelectEntering(SelectEnterEventArgs args)
        {
            base.OnSelectEntering(args);
            if (IsLiquidInteractable(args.interactable))
            {
                var liquidInteractable = args.interactable as ILiquidInteractable;

                if (liquidInteractable.InteractionAction.Equals(InteractionTriggerAction.Select))
                {
                    LiquidPlayerStateMachine.AddControllerState(m_controllerType, liquidInteractable.InteractionState);
                }

                if (this.IsAnimateHands && liquidInteractable.AnimationSettings.AnimateOnSelect)
                {
                    PlayAnimation(liquidInteractable.AnimationSettings.SelectEnterState);
                }
            }
        }

        protected override void OnSelectExiting(SelectExitEventArgs args)
        {
            base.OnSelectExiting(args);
            if (IsLiquidInteractable(args.interactable))
            {
                var liquidInteractable = args.interactable as ILiquidInteractable;

                if (liquidInteractable.InteractionAction.Equals(InteractionTriggerAction.Select))
                {
                    LiquidPlayerStateMachine.RemoveControllerState(m_controllerType, liquidInteractable.InteractionState);
                }

                if (this.IsAnimateHands && liquidInteractable.AnimationSettings.AnimateOnSelect)
                {
                    PlayAnimation(liquidInteractable.AnimationSettings.SelectExitState);
                }
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

        private bool IsLiquidInteractable(XRBaseInteractable interactable)
        {
            return interactable is ILiquidInteractable;
        }
    }
}
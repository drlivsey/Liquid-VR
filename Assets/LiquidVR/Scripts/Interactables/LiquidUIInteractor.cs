using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Liquid.Core;

namespace Liquid.Interactables
{
    public class LiquidUIInteractor : XRRayInteractor
    {
        [SerializeField] private ControllerType m_controllerType = ControllerType.Undefided;
        [SerializeField] private bool m_animateHands = false;
        [SerializeField] private string m_UIInteractionState = string.Empty;
        [SerializeField] private string m_defaultState = string.Empty;

        public bool IsAnimateHands
        {
            get => m_animateHands;
            set => m_animateHands = value;
        }

        private bool _isHoveringUI = false;
        private GameObject _howeringUI = null;
        private Animator _targetAnimator = null;
        private XRInteractorLineVisual _visualLine = null;

        protected override void Start()
        {
            base.Start();
            InitializeComponent();
        }

        private void Update()
        {
            ProcessHovering();
        }

        private void InitializeComponent()
        {
            _visualLine = GetComponent<XRInteractorLineVisual>();
            _targetAnimator = m_controllerType == ControllerType.LeftController ? LiquidPlayer.LeftHandAnimator : LiquidPlayer.RightHandAnimator;
        }

        private void ProcessHovering()
        {
            if (TryGetCurrentUIRaycastResult(out var result))
            {
                if (_howeringUI == result.gameObject)
                    return;
                
                _howeringUI = result.gameObject;
                _isHoveringUI = true;

                ProcessHoverEnter();
            }
            else
            {
                if (_isHoveringUI == false)
                    return;

                _howeringUI = null;
                _isHoveringUI = false;

                ProcessHoverExit();
            }
        }

        private void ProcessHoverEnter()
        {
            if (LiquidPlayerStateMachine.CanTransact(ControllerState.InteractsUI, m_controllerType) == false) return;
            SetToInteraction();
        }

        private void ProcessHoverExit()
        {
            SetToDefault();
        }

        private void SetUIInteractionActivity(bool state)
        {
            this.allowHover = state;
            this.allowSelect = state;

            _visualLine.enabled = state;
        }

        private void SetToInteraction()
        {
            SetUIInteractionActivity(true);
            LiquidPlayerStateMachine.AddControllerState(m_controllerType, ControllerState.Interacts);
            
            if (m_animateHands) PlayAnimation(m_UIInteractionState);
        }

        private void SetToDefault()
        {
            SetUIInteractionActivity(false);
            LiquidPlayerStateMachine.RemoveControllerState(m_controllerType, ControllerState.Interacts);
    
            if (m_animateHands) PlayAnimation(m_defaultState);
        }

        private void PlayAnimation(string stateName)
        {
            _targetAnimator.speed = 1f;
            _targetAnimator.Play(stateName, -1);
        }
    }
}
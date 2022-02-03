using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Liquid.Core;

namespace Liquid.Interactables
{
    [RequireComponent(typeof(XRRayInteractor), typeof(XRController))]
    public class LiquidTeleportInteractor : MonoBehaviour
    {
        #region Inspector
        [SerializeField] private ControllerType m_controllerType = ControllerType.Undefided;
        [SerializeField] private bool m_animateHands = false;
        [SerializeField] private string m_teleportationStateName = string.Empty;
        [SerializeField] private string m_releaseStateName = string.Empty;
        #endregion
        #region Variables
        public bool IsAnimateHands 
        { 
            get => m_animateHands; 
            set => m_animateHands = value; 
        }
        private XRInteractorLineVisual _visualLine = null;
        private XRRayInteractor _targetRayInteractor = null;
        private XRController _targetController = null;
        private Animator _targetAnimator = null;
        private bool _isActive = false;
        #endregion
        #region MonoBehaviour callbacks
        private void Start()
        {
            InitializeComponent();
        }
        
        private void Update()
        {
            ProcessTeleport();
        }

        private void OnEnable()
        {
            LiquidPlayer.OnHandsPairChanged += UpdateAnimator;
        }

        private void OnDisable()
        {
            LiquidPlayer.OnHandsPairChanged -= UpdateAnimator;
        }
        #endregion
        #region Methods
        private void InitializeComponent()
        {
            _targetController = GetComponent<XRController>();
            _targetRayInteractor = GetComponent<XRRayInteractor>();
            _visualLine = GetComponent<XRInteractorLineVisual>();

            _targetRayInteractor.allowHover = false;
            _targetRayInteractor.allowSelect = false;
            _visualLine.enabled = false;

            _targetAnimator = LiquidPlayer.GetHandAnimator(m_controllerType);
        }

        private void ProcessTeleport()
        {
            if (LiquidControllerInput.GetButtonDown(_targetController, _targetController.selectUsage))
            {
                if (LiquidPlayerStateMachine.CanTransact(ControllerState.Teleporting, m_controllerType)) 
                {
                    SetToTeleportState();
                }
            }
            if (LiquidControllerInput.GetButtonUp(_targetController, _targetController.selectUsage) && _isActive)
            {
                if (_isActive)
                {
                    SetToDefaultState();
                }
            }
        }

        private void SetToTeleportState()
        {
            SetTeleportActivity(true);
            LiquidPlayerStateMachine.AddControllerState(m_controllerType, ControllerState.Teleporting);

            if (m_animateHands)
            {
                PlayAnimation(m_teleportationStateName);
            }
        }

        private void SetToDefaultState()
        {
            SetTeleportActivity(false);
            LiquidPlayerStateMachine.RemoveControllerState(m_controllerType, ControllerState.Teleporting);

            if (m_animateHands) 
            {
                PlayAnimation(m_releaseStateName);
            }
        }

        private void SetTeleportActivity(bool state)
        {
            _targetRayInteractor.allowHover = state;
            _targetRayInteractor.allowSelect = state;
            _visualLine.enabled = state;
            _isActive = state;
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
        #endregion
    }
}
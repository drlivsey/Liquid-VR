using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;
using Liquid.Core;

namespace Liquid.Interactables
{
    [RequireComponent(typeof(XRBaseInteractable))]
    public class LiquidHandSelectingEventer : MonoBehaviour
    {
        [SerializeField] private UnityEvent m_onLeftInteractorSelectEntered;
        [SerializeField] private UnityEvent m_onLeftInteractorSelectExited;
        [SerializeField] private UnityEvent m_onRightInteractorSelectEntered;
        [SerializeField] private UnityEvent m_onRightInteractorSelectExited;

        public UnityEvent OnLeftInteractorSelectEntered => m_onLeftInteractorSelectEntered;
        public UnityEvent OnLeftInteractorSelectExited => m_onLeftInteractorSelectExited;
        public UnityEvent OnRightInteractorSelectEntered => m_onRightInteractorSelectEntered;
        public UnityEvent OnRightInteractorSelectExited => m_onRightInteractorSelectExited;

        private XRBaseInteractable _targetInteractable = null;

        private void Awake()
        {
            InitializeComponent();
        }

        private void OnEnable()
        {
            _targetInteractable.selectEntered.AddListener(OnSelectEntered);
            _targetInteractable.selectExited.AddListener(OnSelectExited);
        }

        private void OnDisable()
        {
            _targetInteractable.selectEntered.RemoveListener(OnSelectEntered);
            _targetInteractable.selectExited.RemoveListener(OnSelectExited);
        }

        private void InitializeComponent()
        {
            _targetInteractable = GetComponent<XRBaseInteractable>();
        }

        private void OnSelectEntered(SelectEnterEventArgs args)
        {
            ProcessSelectEnteringInteraction(args.interactor);
        }

        private void OnSelectExited(SelectExitEventArgs args)
        {
            ProcessSelectExitingInteraction(args.interactor);
        }

        private void ProcessSelectEnteringInteraction(XRBaseInteractor interactor)
        {
            if (interactor.Equals(LiquidPlayer.LeftGrabInteractor))
            {
                OnLeftInteractorSelectEntered?.Invoke();
                return;
            }
            if (interactor.Equals(LiquidPlayer.RightGrabInteractor))
            {
                OnRightInteractorSelectEntered?.Invoke();
                return;
            }
        }

        private void ProcessSelectExitingInteraction(XRBaseInteractor interactor)
        {
            if (interactor.Equals(LiquidPlayer.LeftGrabInteractor))
            {
                OnLeftInteractorSelectExited?.Invoke();
            }
            if (interactor.Equals(LiquidPlayer.RightGrabInteractor))
            {
                OnRightInteractorSelectExited?.Invoke();
            }
        }
    }
}
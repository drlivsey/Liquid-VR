using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Liquid.Core;

namespace Liquid.Interactables
{
    public class LiquidGrabInteractable : XRGrabInteractable, ILiquidInteractable
    {
        [SerializeField] LiquidInteractableAnimationSettings m_animationSettings;

        public bool isLocked
        {
            get => _isLocked; 
            protected set => _isLocked = value;
        }

        public XRBaseInteractor lockingInteractor 
        {
            get => _lockingInteractor; 
            protected set => _lockingInteractor = value;
        }

        public ControllerState InteractionState => ControllerState.Interacts;
        public InteractionTriggerAction InteractionAction => InteractionTriggerAction.Select;
        public LiquidInteractableAnimationSettings AnimationSettings => m_animationSettings;

        private bool _isLocked = false;
        private XRBaseInteractor _lockingInteractor = null;

        public override bool IsHoverableBy(XRBaseInteractor interactor)
        {
            if (_isLocked && _lockingInteractor != null)
            {
                return _lockingInteractor.Equals(interactor) && base.IsHoverableBy(interactor);
            }
            return base.IsHoverableBy(interactor);
        }

        public override bool IsSelectableBy(XRBaseInteractor interactor)
        {
            if (_isLocked && _lockingInteractor != null)
            {
                return _lockingInteractor.Equals(interactor) && base.IsSelectableBy(interactor);
            }
            return base.IsSelectableBy(interactor);
        }

        public virtual void LockInteractionsBy(XRBaseInteractor interactor)
        {
            if (_isLocked) return;

            _lockingInteractor = interactor;
            _isLocked = true;
        }

        public virtual void UnlockInteractionsBy(XRBaseInteractor interactor)
        {
            if (_isLocked == false) return;
            if (_lockingInteractor?.Equals(interactor) == false)
            {
                Debug.LogWarning($"You can't unlock interactions by interactor on {interactor.gameObject.name} " 
                            + $"because it's locked by interactor on {_lockingInteractor.gameObject.name}");
            }

            _lockingInteractor = null;
            _isLocked = false;
        }

        public virtual void UnlockInteractionsManualy()
        {
            if (_isLocked == false) return;
            
            _lockingInteractor = null;
            _isLocked = false;
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

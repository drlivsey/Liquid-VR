using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

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

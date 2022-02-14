using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Liquid.Core;

namespace Liquid.Interactables
{
    [AddComponentMenu("Liquid/Interactables/Liquid Switcher Interactor (Model Based)")]
    public class LiquidModelBasedSwitcher : LiquidSwitcherInteractable
    {
        [SerializeField] private Transform m_leftAttach = null;
        [SerializeField] private Transform m_rightAttach = null;

        private Transform _attachTransform = null;
        private ModelBuffer _handBuffer = new ModelBuffer();

        protected override void OnSelectEntering(SelectEnterEventArgs args)
        {
            SetTransform(args.interactor);
            base.OnSelectEntering(args);
            SetupModelBuffer(args.interactor);
            CaptureHand();
        }

        protected override void OnSelectExiting(SelectExitEventArgs args)
        {
            base.OnSelectExiting(args);
            ReleaseHand();
            ClearModelBuffer();
        }

        private void SetTransform(XRBaseInteractor interactor)
        {
            if (interactor.Equals(LiquidPlayer.LeftGrabInteractor))
            {
                _attachTransform = m_leftAttach;
            }
            else if (interactor.Equals(LiquidPlayer.RightGrabInteractor))
            {
                _attachTransform = m_rightAttach;
            }
        }

        private void CaptureHand()
        {
            SetHand(_attachTransform, Vector3.zero, Vector3.zero);
        }

        private void ReleaseHand()
        {
            SetHand(_handBuffer.OriginalParent, _handBuffer.OriginalPosition, _handBuffer.OriginalRotation);
        }

        private void SetHand(Transform parent, Vector3 position, Vector3 rotation)
        {
            _handBuffer.ModelObject.transform.SetParent(parent);
            _handBuffer.ModelObject.transform.localPosition = position;
            _handBuffer.ModelObject.transform.localEulerAngles = rotation;
        }

        private void SetupModelBuffer(XRBaseInteractor interactor)
        {
            GameObject hand = null;

            if (interactor.Equals(LiquidPlayer.LeftGrabInteractor))
            {
                hand = LiquidPlayer.CurrentHandsPair.LeftHand.Prefab;
            }
            else if (interactor.Equals(LiquidPlayer.RightGrabInteractor))
            {
                hand = LiquidPlayer.CurrentHandsPair.RightHand.Prefab;
            }

            _handBuffer.SetupBuffer(hand);
        }

        private void ClearModelBuffer()
        {
            _handBuffer.ClearBuffer();
        }
    }
}
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;
using Liquid.Core;

namespace Liquid.Interactables
{
    public class LiquidTargetableSocket : XRSocketInteractor
    {
        [SerializeField, Space(10)] private GameObject m_targetObject = null;
        [SerializeField] private Action m_placingAction = Action.None;
        [SerializeField] private SelectingObjects m_canSelect = SelectingObjects.OnlyTargetObject;
        [SerializeField, TagSelector] private string m_targetTag = string.Empty;
        [SerializeField] private bool m_isValidTargetPlaced = false;
        
        [Space(10)] public UnityEvent OnTargetObjectPlaced;
        public UnityEvent OnTargetObjectRemoved;

        public bool IsValidTargetPlaced
        {
            get => m_isValidTargetPlaced;
            private set => m_isValidTargetPlaced = value;
        }

        public override bool CanSelect(XRBaseInteractable interactable)
        {
            if (interactable == null)
            {
                return base.CanSelect(interactable);
            }
            switch (m_canSelect)
            {
                case SelectingObjects.None: 
                    return false;
                case SelectingObjects.OnlyTargetObject:
                    return (interactable.gameObject.Equals(m_targetObject) && base.CanSelect(interactable));
                case SelectingObjects.AllWithTargetTag:
                    return (interactable.CompareTag(m_targetTag) && base.CanSelect(interactable));
                default:
                    return base.CanSelect(interactable);
            }
        }

        public override bool CanHover(XRBaseInteractable interactable)
        {
            if (interactable == null) 
            {
                return base.CanHover(interactable);
            }
            switch (m_canSelect)
            {
                case SelectingObjects.None:
                    return false;
                case SelectingObjects.OnlyTargetObject:
                    return (interactable.gameObject.Equals(m_targetObject) && base.CanHover(interactable));
                case SelectingObjects.AllWithTargetTag:
                    return (interactable.CompareTag(m_targetTag) && base.CanHover(interactable));
                default:
                    return base.CanHover(interactable);
            }
        }

        protected override void OnSelectEntering(SelectEnterEventArgs args)
        {
            base.OnSelectEntering(args);
            if (args.interactable.gameObject.Equals(m_targetObject))
            {
                m_isValidTargetPlaced = true;
                OnTargetObjectPlaced?.Invoke();
            }
            switch (m_placingAction)
            {
                case Action.LockObjectInteractions:
                {
                    if (args.interactable is LiquidGrabInteractable)
                    {
                        (args.interactable as LiquidGrabInteractable).LockInteractionsBy(this);
                    }
                    break;
                }
                case Action.DisableObject:
                {
                    args.interactable.enabled = false;
                    args.interactable.gameObject.SetActive(false);
                    break;
                }
                case Action.DestroyObject:
                {
                    args.interactable.enabled = false;
                    args.interactable.gameObject.SetActive(false);
                    Destroy(args.interactable.gameObject);
                    break;
                }
                default: return;
            }
        }

        protected override void OnSelectExiting(SelectExitEventArgs args)
        {
            base.OnSelectExiting(args);
            if (args.interactable.gameObject.Equals(m_targetObject))
            {
                m_isValidTargetPlaced = false;
                OnTargetObjectRemoved?.Invoke();
            }
        }


        private enum SelectingObjects
        {
            OnlyTargetObject = 0,
            AllWithTargetTag = 1,
            None = 2
        }

        private enum Action 
        {
            None = 0,
            DisableObject = 1,
            DestroyObject = 2,
            LockObjectInteractions = 3
        }
    }
}
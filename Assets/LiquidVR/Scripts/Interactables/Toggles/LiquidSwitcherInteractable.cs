using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Liquid.Core;

namespace Liquid.Interactables
{
    public class LiquidSwitcherInteractable : XRSimpleInteractable, ILiquidInteractable
    {
        [SerializeField] private LiquidSwitcher m_targetSwitcher;
        [SerializeField] private LiquidInteractableAnimationSettings m_animationSettings;
        
        public InteractionTriggerAction InteractionAction => InteractionTriggerAction.Select;
        public LiquidInteractableAnimationSettings AnimationSettings => m_animationSettings;
        public ControllerState InteractionState => ControllerState.Interacts;

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
            if (m_targetSwitcher) m_targetSwitcher.IsPressed = true;
        }

        protected override void OnSelectExited(SelectExitEventArgs args)
        {
            base.OnSelectExited(args);
            if (m_targetSwitcher) m_targetSwitcher.IsPressed = false;
        }
    }
}
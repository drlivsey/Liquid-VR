using Liquid.Core;

namespace Liquid.Interactables
{
    public interface ILiquidInteractable
    {
        ControllerState InteractionState { get; }
        InteractionTriggerAction InteractionAction { get; }
        LiquidInteractableAnimationSettings AnimationSettings { get; }
    }
}
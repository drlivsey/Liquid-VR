using UnityEngine;

namespace Liquid.Interactables
{
    public class LiquidButton : LiquidBaseToggle
    {
        protected virtual void OnTriggerEnter(Collider other) 
        {
            if (other.TryGetComponent<LiquidPlayerFinger>(out var finger))
            {
                IsPressed = true;
            }
        }

        protected virtual void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<LiquidPlayerFinger>(out var finger))
            {
                IsPressed = false;
            }
        }
    }

    public enum LiquidHandInteraction
    {
        Hold = 0,
        Touch = 1
    }
}
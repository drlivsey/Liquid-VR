using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Liquid.Interactables
{
    public class LiquidTumbler : LiquidBaseToggle
    {
        protected virtual void OnTriggerEnter(Collider other) 
        {
            if (other.TryGetComponent<LiquidPlayerFinger>(out var finger))
            {
                if (InteractionType == LiquidHandInteraction.Touch)
                {
                    IsPressed = !IsPressed;
                    return;
                }
                IsPressed = true;
            }
        }

        protected virtual void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<LiquidPlayerFinger>(out var finger))
            {
                if (InteractionType == LiquidHandInteraction.Touch) return;
                IsPressed = false;
            }
        }
    }
}
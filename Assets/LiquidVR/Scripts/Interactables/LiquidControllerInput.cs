using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

namespace Liquid.Interactables
{
    public static class LiquidControllerInput 
    {
        private static Dictionary<XRController, List<InputDeviceButtonValue>> _controllerButtons = new Dictionary<XRController, List<InputDeviceButtonValue>>();

        public static bool GetButtonDown(XRController controller, InputHelpers.Button button)
        {
            if (InputHelpers.IsPressed(controller.inputDevice, button, out var isPressed) == false) 
            {
                return false;
            }

            var buttonValue = GetControllerInputFeatureValue(controller, button);

            if (isPressed)
            {
                if (buttonValue.isPressed) 
                {
                    return false;
                }
                
                buttonValue.isPressed = true;
                SetControllerFeatureValue(controller, buttonValue);
                return true;
            }
            else
            {
                buttonValue.isPressed = false;
                SetControllerFeatureValue(controller, buttonValue);
                return false;
            }
        }

        public static bool GetButtonUp(XRController controller, InputHelpers.Button button)
        {
            if (InputHelpers.IsPressed(controller.inputDevice, button, out var isPressed) == false) 
            {
                return false;
            }

            var buttonValue = GetControllerInputFeatureValue(controller, button);
            
            if (isPressed == false)
            {
                if (buttonValue.isReleased) 
                {
                    return false;
                }
                
                buttonValue.isReleased = true;
                SetControllerFeatureValue(controller, buttonValue);
                return true;
            }
            else
            {
                buttonValue.isReleased = false;
                SetControllerFeatureValue(controller, buttonValue);
                return false;
            }
        }

        public static bool GetButtonValue(XRController controller, InputHelpers.Button button)
        {
            if (InputHelpers.IsPressed(controller.inputDevice, button, out var isPressed)) return isPressed;
            else return false;
        }

        private static InputDeviceButtonValue GetControllerInputFeatureValue(XRController controller, InputHelpers.Button button)
        {
            if (_controllerButtons.ContainsKey(controller))
            {
                if (_controllerButtons[controller].Any(x => x.Button == button)) 
                {
                    return _controllerButtons[controller].First(x => x.Button == button);
                }
                else 
                {
                    var buttonValue = new InputDeviceButtonValue() 
                    { 
                        Button = button, 
                        isPressed = false, 
                        isReleased = true 
                    };
                    _controllerButtons[controller].Add(buttonValue);
                    return buttonValue;
                }
            }
            else 
            {
                var buttonValue = new InputDeviceButtonValue() 
                { 
                    Button = button, 
                    isPressed = false, 
                    isReleased = true 
                };
                _controllerButtons.Add(controller, new List<InputDeviceButtonValue>() { buttonValue });
                return buttonValue;
            }
        }

        private static void SetControllerFeatureValue(XRController controller, InputDeviceButtonValue value)
        {
            var featuresValues = _controllerButtons[controller];
            for (var i = 0; i < featuresValues.Count; i++)
            {
                if (featuresValues[i].Button != value.Button) 
                {
                    continue;
                }
                
                featuresValues[i] = value;
            }
            _controllerButtons[controller] = featuresValues;
        }

        private struct InputDeviceButtonValue
        {
            public InputHelpers.Button Button;
            public bool isPressed;
            public bool isReleased;
        }
    }
}
using System.Collections.Generic;
using UnityEngine;
using Liquid.Core;

//  Superglobal singleton class,
//  which regulates player interactions

namespace Liquid.Interactables
{
    public class LiquidPlayerStateMachine : MonoBehaviour
    {
        #region Inspector
        [SerializeField] private List<ControllerState> m_leftControllerStates = new List<ControllerState>();
        [SerializeField] private List<ControllerState> m_rightControllerStates = new List<ControllerState>();
        #endregion
        #region Variables
        private static LiquidPlayerStateMachine _instance = null;
        #endregion
        #region MonoBehaviour callbacks
        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
            }
            else if (_instance == this)
            {
                Destroy(gameObject);
            }
        }
        #endregion
        #region Methods
        public static bool CanTransact(ControllerState state, ControllerType type)
        {
            switch (state)
            {
                case ControllerState.Teleporting: return CanHandTeleport(type);
                case ControllerState.Interacts : return CanHandInteracts(type);
                case ControllerState.HoldAnItem : return CanHandGrab(type);
                case ControllerState.InteractsUI : return CanHandInteractsUI(type);
                case ControllerState.Clench : return CanHandClench(type);
                default: return false;
            }
        }
        private static bool CanHandTeleport(ControllerType type)
        {
            if (type == ControllerType.Undefided)
                throw new System.Exception("Controller type can not be undefined!");

            if (type == ControllerType.LeftController)
            {
                if (_instance.m_leftControllerStates.Contains(ControllerState.Interacts) || 
                    _instance.m_leftControllerStates.Contains(ControllerState.InteractsUI) ||
                    _instance.m_rightControllerStates.Contains(ControllerState.Teleporting) ||
                    _instance.m_rightControllerStates.Contains(ControllerState.InteractsUI) || 
                    _instance.m_leftControllerStates.Contains(ControllerState.Clench) || 
                    _instance.m_leftControllerStates.Contains(ControllerState.HoldAnItem))
                        return false;
            }

            if (type == ControllerType.RightController)
            {
                if (_instance.m_rightControllerStates.Contains(ControllerState.Interacts) || 
                    _instance.m_rightControllerStates.Contains(ControllerState.InteractsUI) || 
                    _instance.m_leftControllerStates.Contains(ControllerState.InteractsUI) ||
                    _instance.m_leftControllerStates.Contains(ControllerState.Teleporting) ||
                    _instance.m_rightControllerStates.Contains(ControllerState.Clench) || 
                    _instance.m_rightControllerStates.Contains(ControllerState.HoldAnItem))
                        return false;
            }

            return true;
        }

        private static bool CanHandInteracts(ControllerType type)
        {
            if (type == ControllerType.Undefided)
                throw new System.Exception("Controller type can not be undefined!");

            if (type == ControllerType.LeftController)
            {
                if (_instance.m_leftControllerStates.Contains(ControllerState.Interacts) ||
                    _instance.m_leftControllerStates.Contains(ControllerState.InteractsUI) ||
                    _instance.m_leftControllerStates.Contains(ControllerState.HoldAnItem) ||
                    _instance.m_leftControllerStates.Contains(ControllerState.Teleporting) ||
                    _instance.m_leftControllerStates.Contains(ControllerState.Clench))
                        return false;
            }

            if (type == ControllerType.RightController)
            {
                if (_instance.m_rightControllerStates.Contains(ControllerState.Interacts) ||
                    _instance.m_rightControllerStates.Contains(ControllerState.InteractsUI) ||
                    _instance.m_rightControllerStates.Contains(ControllerState.HoldAnItem) ||
                    _instance.m_rightControllerStates.Contains(ControllerState.Teleporting) ||
                    _instance.m_rightControllerStates.Contains(ControllerState.Clench))
                        return false;
            }

            return true;
        }

        private static bool CanHandGrab(ControllerType type)
        {
            if (type == ControllerType.Undefided)
                throw new System.Exception("Controller type can not be undefined!");

            if (type == ControllerType.LeftController)
            {
                if (_instance.m_leftControllerStates.Contains(ControllerState.Interacts) ||
                    _instance.m_leftControllerStates.Contains(ControllerState.Teleporting) ||
                    _instance.m_leftControllerStates.Contains(ControllerState.Clench))
                        return false;
            }

            if (type == ControllerType.RightController)
            {
                if (_instance.m_rightControllerStates.Contains(ControllerState.Interacts) ||
                    _instance.m_rightControllerStates.Contains(ControllerState.Teleporting) ||
                    _instance.m_rightControllerStates.Contains(ControllerState.Clench))
                        return false;
            }

            return true;
        }

        private static bool CanHandInteractsUI(ControllerType type)
        {
            if (type == ControllerType.Undefided)
                throw new System.Exception("Controller type can not be undefined!");

            if (type == ControllerType.LeftController)
            {
                if (_instance.m_leftControllerStates.Contains(ControllerState.Interacts) ||
                    _instance.m_leftControllerStates.Contains(ControllerState.Teleporting) ||
                    _instance.m_rightControllerStates.Contains(ControllerState.Teleporting) ||
                    _instance.m_leftControllerStates.Contains(ControllerState.Clench) || 
                    _instance.m_leftControllerStates.Contains(ControllerState.HoldAnItem))
                        return false;
            }

            if (type == ControllerType.RightController)
            {
                if (_instance.m_rightControllerStates.Contains(ControllerState.Interacts) ||
                    _instance.m_rightControllerStates.Contains(ControllerState.Teleporting) ||
                    _instance.m_leftControllerStates.Contains(ControllerState.Teleporting) ||
                    _instance.m_rightControllerStates.Contains(ControllerState.Clench) || 
                    _instance.m_rightControllerStates.Contains(ControllerState.HoldAnItem))
                        return false;
            }

            return true;
        }

        private static bool CanHandClench(ControllerType type)
        {
            if (type == ControllerType.LeftController)
            {
                if (_instance.m_leftControllerStates.Contains(ControllerState.Interacts) ||
                    _instance.m_leftControllerStates.Contains(ControllerState.InteractsUI) ||
                    _instance.m_leftControllerStates.Contains(ControllerState.HoldAnItem) ||
                    _instance.m_leftControllerStates.Contains(ControllerState.Teleporting))
                        return false;
            }

            if (type == ControllerType.RightController)
            {
                if (_instance.m_rightControllerStates.Contains(ControllerState.Interacts) ||
                    _instance.m_rightControllerStates.Contains(ControllerState.InteractsUI) ||
                    _instance.m_rightControllerStates.Contains(ControllerState.HoldAnItem) ||
                    _instance.m_rightControllerStates.Contains(ControllerState.Teleporting))
                        return false;
            }
            
            return true;
        }

        public static void AddControllerState(ControllerType type, ControllerState state)
        {
            if (type == ControllerType.Undefided)
            {
                throw new System.Exception("Controller type can not be undefined!");
            }
            else if (type == ControllerType.LeftController)
            {
                if (_instance.m_leftControllerStates.Contains(state))
                    return;
                
                _instance.m_leftControllerStates.Add(state);
            }
            else if (type == ControllerType.RightController)
            {
                if (_instance.m_rightControllerStates.Contains(state))
                    return;
                
                _instance.m_rightControllerStates.Add(state);
            }
            else if (type == ControllerType.Both)
            {
                if (_instance.m_leftControllerStates.Contains(state) || _instance.m_rightControllerStates.Contains(state))
                    return;
                
                _instance.m_leftControllerStates.Add(state);
                _instance.m_rightControllerStates.Add(state);
            }
        }

        public static void RemoveControllerState(ControllerType type, ControllerState state)
        {
            if (type == ControllerType.Undefided)
            {
                throw new System.Exception("Controller type can not be undefined!");
            }
            else if (type == ControllerType.LeftController)
            {
                _instance.m_leftControllerStates.Remove(state);
            }
            else if (type == ControllerType.RightController)
            {
                _instance.m_rightControllerStates.Remove(state);
            }
            else if (type == ControllerType.Both)
            {
                _instance.m_leftControllerStates.Remove(state);
                _instance.m_rightControllerStates.Remove(state);
            }
        }
        #endregion
    }
}
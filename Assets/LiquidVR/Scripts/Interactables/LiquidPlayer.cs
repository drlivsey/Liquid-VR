using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;
using Liquid.Core;

namespace Liquid.Interactables
{
    public class LiquidPlayer : MonoBehaviour
    {
        #region Inspector
        [SerializeField, Header("Left hand interactables"), Space(10)] private XRController m_leftHandController = null;
        [SerializeField] private XRDirectInteractor m_leftHandGrabInteractor = null;
        [SerializeField] private XRRayInteractor m_leftHandTeleport = null;
        [SerializeField, Header("Right hand interactables"), Space(10)] private XRController m_rightHandController = null;
        [SerializeField] private XRDirectInteractor m_rightHandGrabInteractor = null;
        [SerializeField] private XRRayInteractor m_rightHandTeleport = null;
        [SerializeField] private HandsPair[] m_handsPairs = null;
        #endregion
        
        #region Variables
        public static event UnityAction OnHandsPairChanged;
        public static XRDirectInteractor LeftGrabInteractor => _instance.m_leftHandGrabInteractor;
        public static XRDirectInteractor RightGrabInteractor => _instance.m_rightHandGrabInteractor;
        public static XRRayInteractor LeftTeleportInteractor => _instance.m_leftHandTeleport;
        public static XRRayInteractor RightTeleportInteractor => _instance.m_rightHandTeleport;
        public static Animator LeftHandAnimator => _currentHandsPair.LeftHand.Animator;
        public static Animator RightHandAnimator => _currentHandsPair.RightHand.Animator;
        public static XRController LeftController => _instance.m_leftHandController;
        public static XRRayInteractor LeftTeleport => _instance.m_leftHandTeleport;
        public static XRController RightController => _instance.m_rightHandController;
        public static XRRayInteractor RightTeleport => _instance.m_rightHandTeleport;
        private static HandsPair _currentHandsPair;
        private static LiquidPlayer _instance = null;
        #endregion
        
        #region MonoBehaviour callbacks
        private void Awake()
        {
            // Singletone instance
            if (_instance == null)
            {
                _instance = this;
            }
            else
            {   
                Destroy(this.gameObject);
            }

           _currentHandsPair = m_handsPairs[0];
           SwitchHandsTo(0);
        }
        #endregion
        
        #region Methods
        public static void SetHandsMaterial(Material material)
        {
            foreach (var handsPair in _instance.m_handsPairs)
            {
                handsPair.SetMaterial(material);
            }
        }

        public static void SwitchHandsTo(int index)
        {
            if (TrySwitchHandsTo(index) == false)
            {
                throw new IndexOutOfRangeException($"Index {index} is out of range {nameof(m_handsPairs)} array");
            }

            OnHandsPairChanged?.Invoke();
        }

        public static bool TrySwitchHandsTo(int index)
        {
            if (index >= _instance.m_handsPairs.Length)
            {
                return false;
            }
            
            _currentHandsPair.Hide();
            _currentHandsPair = _instance.m_handsPairs[index];
            _currentHandsPair.Show();
            
            ApplyAttach(_instance.m_leftHandGrabInteractor, _currentHandsPair.LeftHand.Attach);
            ApplyAttach(_instance.m_rightHandGrabInteractor, _currentHandsPair.RightHand.Attach);

            return true;
        }
        
        public static void SwitchHandsTo(string pairName)
        {
            if (TrySwitchHandsTo(pairName) == false)
            {
                throw new IndexOutOfRangeException($"{nameof(m_handsPairs)} array doesn't contains hands pair with name {pairName}");
            }

            OnHandsPairChanged?.Invoke();
        }

        public static bool TrySwitchHandsTo(string pairName)
        {
            if (_instance.m_handsPairs.Any(x => x.Name == pairName) == false)
            {
                return false;
            }
            
            _currentHandsPair.Hide();
            _currentHandsPair = _instance.m_handsPairs.First(x => x.Name == pairName);
            _currentHandsPair.Show();
            
            ApplyAttach(_instance.m_leftHandGrabInteractor, _currentHandsPair.LeftHand.Attach);
            ApplyAttach(_instance.m_rightHandGrabInteractor, _currentHandsPair.RightHand.Attach);

            return true;
        }

        public static void HideCurrentHands()
        {
            if (_currentHandsPair == null) return;
            _currentHandsPair.Hide();
        }

        public static void ShowCurrentHands()
        {
            if (_currentHandsPair == null) return;
            _currentHandsPair.Show();
        }

        private static void ApplyAttach(XRDirectInteractor interactor, Transform attach)
        {
            interactor.attachTransform = attach;
        }

        public static Animator GetHandAnimator(ControllerType controller)
        {
            return controller == ControllerType.LeftController ? LiquidPlayer.LeftHandAnimator : LiquidPlayer.RightHandAnimator;
        }

        #endregion
    }

    [Serializable] public class HandsPair
    {
        #region Inspector
        [SerializeField] private string m_name = "Default hands";
        [SerializeField] private Hand m_leftHand = null;
        [SerializeField] private Hand m_rightHand = null;
        

        public string Name => m_name;
        public Hand LeftHand => m_leftHand;
        public Hand RightHand => m_rightHand;
        #endregion
        
        #region Methods
        public void Show()
        {
            m_leftHand.Show();
            m_rightHand.Show();
        }

        public void Hide()
        {
            m_leftHand.Hide();
            m_rightHand.Hide();
        }
        public void SetMaterial(Material material)
        {
            m_leftHand.SetMaterial(material);
            m_rightHand.SetMaterial(material);
        }
        #endregion
    }

    [Serializable] public class Hand
    {
        #region Inspector
        [SerializeField] private GameObject m_prefab = null;
        [SerializeField] private Transform m_attach = null;
        [SerializeField] private Renderer m_renderer = null;
        #endregion
        #region Variables
        public Animator Animator 
        { 
            get 
            {
                if (_handAnimator == null) 
                {
                    _handAnimator = m_prefab.GetComponentInChildren<Animator>(true);
                }
                return _handAnimator;
            }
        }
        public Transform Attach => m_attach;
        private Animator _handAnimator = null;
        #endregion
        #region Methods
        public void Show()
        {
            m_prefab.SetActive(true);
            SetRenderersState(true);
        }

        public void Hide()
        {
            m_prefab.SetActive(false);
            SetRenderersState(false);
        }
        public void SetMaterial(Material material)
        {
            m_renderer.material = material;
        }

        private void SetRenderersState(bool state)
        {
            var renderers = GetRenderers();
            
            foreach (var renderer in renderers)
            {
                renderer.enabled = state;
            }
        }
        private Renderer[] GetRenderers()
        {
            return m_prefab.GetComponentsInChildren<Renderer>(true);
        }
        #endregion
    }
}
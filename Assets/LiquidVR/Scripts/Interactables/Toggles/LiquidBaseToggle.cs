using UnityEngine;
using UnityEngine.Events;

namespace Liquid.Interactables
{
    [RequireComponent(typeof(Collider), typeof(Rigidbody))]
    public class LiquidBaseToggle : MonoBehaviour
    {
        [SerializeField] private LiquidHandInteraction m_interactionType = LiquidHandInteraction.Touch;
        [SerializeField] private bool m_isPressed = false;
        
        public LiquidHandInteraction InteractionType
        {
            get => m_interactionType;
            set => m_interactionType = value;
        }

        public bool IsPressed
        {
            get => m_isPressed;
            set 
            {
                if (value) OnPress?.Invoke();
                else OnRelease?.Invoke();
                m_isPressed = value;
            }
        }

        public UnityEvent OnPress;
        public UnityEvent OnRelease;

        private Collider _targetCollider = null;
        private Rigidbody _targetRigidbody = null;
        private bool _initialized = false;

        protected virtual void Awake()
        {
            InitializeComponent();
        }

        public virtual void EnableInteractions()
        {
            if (_initialized == false) InitializeComponent();
            _targetCollider.enabled = true;
        }

        public virtual void DisableInteractions()
        {
            if (_initialized == false) InitializeComponent();
            _targetCollider.enabled = false;
        }

        protected virtual void  InitializeComponent()
        {
            if(_initialized) return;

            _targetCollider = GetComponent<Collider>();
            _targetCollider.isTrigger = true;
            
            _targetRigidbody = GetComponent<Rigidbody>();
            _targetRigidbody.isKinematic = true;
            _targetRigidbody.useGravity = false;

            _initialized = true;
        }
    }
}
using UnityEngine;

namespace Liquid.Interactables
{
    [RequireComponent(typeof(Collider), typeof(Rigidbody))]
    public class LiquidPlayerFinger : MonoBehaviour
    {
        private Collider _targetCollider = null;
        private Rigidbody _targetRigidbody = null;

        private void Awake()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            _targetCollider = GetComponent<Collider>();
            _targetCollider.isTrigger = true;

            _targetRigidbody = GetComponent<Rigidbody>();
            _targetRigidbody.isKinematic = true;
            _targetRigidbody.useGravity = false;
        }
    }
}
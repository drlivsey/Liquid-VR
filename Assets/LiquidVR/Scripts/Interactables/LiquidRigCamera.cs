using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Liquid.Interactables
{
    [RequireComponent(typeof(Rigidbody), typeof(Collider), typeof(Camera))]
    public class LiquidRigCamera : MonoBehaviour
    {
        private Rigidbody _headRigidbody = null;
        private Collider _headCollider = null;

        private void Awake()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            _headCollider = GetComponent<Collider>();
            _headCollider.isTrigger = true;

            _headRigidbody = GetComponent<Rigidbody>();
            _headRigidbody.isKinematic = true;
            _headRigidbody.useGravity = false;
        }
    }
}
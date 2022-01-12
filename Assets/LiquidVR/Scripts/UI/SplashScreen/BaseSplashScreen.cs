using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Liquid.UI
{
    public class BaseSplashScreen : MonoBehaviour
    {
        [SerializeField] private Transform m_targetSplash = null;
        [SerializeField] private SplashCanvas m_splashCanvas = null;
        [SerializeField, Range(0.01f, 5f)] private float m_followSpeed = 0.1f;

        private Transform _mainCameraTransform = null;

        private void Awake()
        {
            _mainCameraTransform = Camera.main.transform;
        }

        protected virtual void Update()
        {
            UpdatePosition();
            UpdateRotation();
        }

        public virtual void Show()
        {
            m_splashCanvas.Show();
        }

        public virtual void Hide()
        {
            m_splashCanvas.Hide();
        }

        protected virtual void UpdatePosition()
        {
            var newRotation = Vector3.zero;
            newRotation.y = Mathf.MoveTowardsAngle(m_targetSplash.eulerAngles.y, _mainCameraTransform.eulerAngles.y, m_followSpeed);
            m_targetSplash.eulerAngles = newRotation;
        }

        protected virtual void UpdateRotation()
        {
            m_splashCanvas.transform.LookAt(_mainCameraTransform);
        }
    }
}
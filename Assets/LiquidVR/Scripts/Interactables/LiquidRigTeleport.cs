using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Liquid.Interactables
{
    public class LiquidRigTeleport : MonoBehaviour
    {
        [SerializeField] private TeleportationProvider m_teleportationProvider = null;
        [SerializeField] private Transform m_teleportAnchorTransform = null;
        [SerializeField] private MatchOrientation m_matchOrientation = MatchOrientation.WorldSpaceUp;

        private void Awake()
        {
            InitializeComponent();
        }

        private void OnValidate() 
        {
            if (m_teleportAnchorTransform) 
            {
                return;
            }
            m_teleportAnchorTransform = this.transform;
        }

        protected void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            GizmoHelpers.DrawWireCubeOriented(m_teleportAnchorTransform.position, m_teleportAnchorTransform.rotation, 1f);
            GizmoHelpers.DrawAxisArrows(m_teleportAnchorTransform, 1f);
        }

        public void TeleportPlayer()
        {
            SendTeleportRequest();
        }

        private void InitializeComponent()
        {
            if (m_teleportationProvider == null)
            {
                m_teleportationProvider = FindObjectOfType<TeleportationProvider>(true);
            }
        }

        private void SendTeleportRequest()
        {
            if (TryGenerateTeleportRequest(out var request))
            {
                m_teleportationProvider.QueueTeleportRequest(request);
            }
        }

        private bool TryGenerateTeleportRequest(out TeleportRequest request)
        {
            if (m_teleportationProvider == null)
            {
                request = default(TeleportRequest);
                return false;
            }
            request = new TeleportRequest
            {
                matchOrientation = m_matchOrientation,
                requestTime = Time.time,
                destinationPosition = m_teleportAnchorTransform.position,
                destinationRotation = m_teleportAnchorTransform.rotation
            };
            return true;
        }
    }
}
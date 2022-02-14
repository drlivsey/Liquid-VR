using System;
using UnityEngine;

namespace Liquid.Core
{
    public struct ModelBuffer
    {
        public GameObject ModelObject;
        public Transform OriginalParent;
        public Vector3 OriginalPosition;
        public Vector3 OriginalRotation;

        public void SetupBuffer(GameObject model)
        {
            this.ModelObject = model;
            this.OriginalParent = model.transform.parent;
            this.OriginalPosition = model.transform.localPosition;
            this.OriginalRotation = model.transform.localEulerAngles;
        }

        public void ClearBuffer()
        {
            this.ModelObject = null;
            this.OriginalParent = null;
            this.OriginalPosition = Vector3.zero;
            this.OriginalRotation = Vector3.zero;
        }
    }

    [Serializable]
    public struct LiquidInteractableAnimationSettings
    {
        public bool AnimateOnHover;
        public string HoverEnterState;
        public string HoverExitState;
        public bool AnimateOnSelect;
        public string SelectEnterState;
        public string SelectExitState;
    }
}
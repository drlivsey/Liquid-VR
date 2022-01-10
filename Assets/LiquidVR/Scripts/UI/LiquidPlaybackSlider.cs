using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Liquid.UI
{
    public class LiquidPlaybackSlider : Slider
    {
        [SerializeField] private UnityEvent m_onHandlePointerDown = new UnityEvent();
        [SerializeField] private UnityEvent m_onHandlePointerUp = new UnityEvent();

        public UnityEvent OnHandlePointerDown => m_onHandlePointerDown;
        public UnityEvent OnHandlePointerUp => m_onHandlePointerUp;

        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);
            OnHandlePointerDown?.Invoke();
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            base.OnPointerUp(eventData);
            OnHandlePointerUp?.Invoke();
        }
    }
}
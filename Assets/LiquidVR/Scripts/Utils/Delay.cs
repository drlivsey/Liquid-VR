using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Liquid.Utils
{
    public class Delay : MonoBehaviour
    {
        [SerializeField, Tooltip("Delay in seconds")] private float m_delay = 1f;
        [SerializeField] private bool m_waitRealtime = false;
        [SerializeField] private bool m_enableOnStart = false;
        [SerializeField] private UnityEvent m_onCountdownBegin = new UnityEvent();
        [SerializeField] private UnityEvent m_onCountdownEnd = new UnityEvent();

        public UnityEvent OnCountdownBegin => m_onCountdownBegin;
        public UnityEvent OnCountdownEnd => m_onCountdownEnd;

        private Coroutine _countdownCoroutine = null;

        private void Start()
        {
            if (m_enableOnStart) BeginCountdown();
        }

        public void BeginCountdown()
        {
            StopCountdown();
            StartCoroutine(WaitForDelay(m_delay, m_waitRealtime));
        }

        public void BeginCountdown(float delay)
        {
            StopCountdown();
            StartCoroutine(WaitForDelay(delay, m_waitRealtime));
        }

        public void BeginCountdown(float delay, bool isRealtime = false)
        {
            StopCountdown();
            StartCoroutine(WaitForDelay(delay, isRealtime));
        }

        public void StopCountdown()
        {
            if (_countdownCoroutine != null) StopCoroutine(_countdownCoroutine);
        }

        private IEnumerator WaitForDelay(float delay, bool inRealtime)
        {
            OnCountdownBegin?.Invoke();
            if (inRealtime) yield return new WaitForSecondsRealtime(delay);
            else yield return new WaitForSeconds(delay);
            OnCountdownEnd?.Invoke();
        }
    }
}
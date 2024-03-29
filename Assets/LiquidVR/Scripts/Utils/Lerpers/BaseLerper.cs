using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Liquid.Utils.Lerpers
{
    public abstract class BaseLerper : MonoBehaviour
    {
        [SerializeField] protected float m_lerpTime = 1f;
        [SerializeField] private UnityEvent m_onLerpStart = new UnityEvent();
        [SerializeField] private UnityEvent m_onLerpEnd = new UnityEvent();
        [SerializeField] private UnityEvent m_onLerpStop = new UnityEvent();
        [SerializeField] private UnityEvent m_onLerpPause = new UnityEvent();
        [SerializeField] private UnityEvent m_onLerpResume = new UnityEvent();

        public UnityEvent OnLerpStart => m_onLerpStart;
        public UnityEvent OnLerpEnd => m_onLerpEnd;
        public UnityEvent OnLerpStoped => m_onLerpStop;
        public UnityEvent OnLerpPause => m_onLerpPause;
        public UnityEvent OnLerpResume => m_onLerpResume;

        public float LerpTime
        {
            get => m_lerpTime;
            set => m_lerpTime = value;
        }

        public float CurrentLerp { get; private set; } = 0f;

        private Coroutine _lerpRoutine;

        public virtual void StartLerp()
        {
            if (_lerpRoutine != null)
            {
                StopCoroutine(_lerpRoutine);
            }

            CurrentLerp = 0f;
            _lerpRoutine = StartCoroutine(LerpRoutine());
        }

        public virtual void StopLerp()
        {
            if (_lerpRoutine == null) return;
            StopCoroutine(_lerpRoutine);
            CurrentLerp = 0f;
            OnLerpStoped?.Invoke();
        }

        public virtual void ResetLerp()
        {
            if (_lerpRoutine == null) return;
            CurrentLerp = 0f;
            LerpValue(0f);
        }

        public virtual void PauseLerp()
        {
            if (_lerpRoutine == null) return;
            StopCoroutine(_lerpRoutine);
            OnLerpPause?.Invoke();
        }

        public virtual void ResumeLerp()
        {
            if (_lerpRoutine == null) return;

            StopCoroutine(_lerpRoutine);
            _lerpRoutine = StartCoroutine(LerpRoutine());
            OnLerpResume?.Invoke();
        }

        protected virtual IEnumerator LerpRoutine()
        {
            var waitFor = new WaitForEndOfFrame();
            OnLerpStart?.Invoke();
            while (CurrentLerp <= m_lerpTime)
            {
                LerpValue(CurrentLerp / m_lerpTime);
                CurrentLerp += Time.deltaTime;
                yield return waitFor;
            }
            LerpValue(1f);
            OnLerpEnd?.Invoke();
        }

        protected abstract void LerpValue(float t);
    }
}
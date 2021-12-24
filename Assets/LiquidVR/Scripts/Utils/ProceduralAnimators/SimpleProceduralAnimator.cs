using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Liquid.Core;

namespace Liquid.Utils
{
    public abstract class SimpleProceduralAnimator : BaseProceduralAnimator
    {
        [SerializeField] private ProceduralAnimation m_animationType = ProceduralAnimation.Single;
        [SerializeField] protected Vector3 m_beginValue = Vector3.zero;
        [SerializeField] protected Vector3 m_endValue = Vector3.one;
        [SerializeField] private float m_animationTime = 1f;
        [SerializeField] private bool m_playOnEnable = false;
        [SerializeField] private UnityEvent m_onStart;
        [SerializeField] private UnityEvent m_onPause;
        [SerializeField] private UnityEvent m_onResume;
        [SerializeField] private UnityEvent m_onStop;
        [SerializeField] private UnityEvent m_onAnimationBegin;
        [SerializeField] private UnityEvent m_onAnimationEnd;

        public bool IsPlaying
        {
            get; protected set;
        }

        public UnityEvent OnStart => m_onStart;
        public UnityEvent OnPause => m_onPause;
        public UnityEvent OnResume => m_onResume;
        public UnityEvent OnStop => m_onStop;
        public UnityEvent OnAnimationBegin => m_onAnimationBegin;
        public UnityEvent OnAnimationEnd => m_onAnimationEnd;

        protected float _currentProcess = 0f;
        protected Direction _animationDirection = Direction.Forward;
        protected Transform _targetTransform = null;
        protected Coroutine _animatingRoutine = null;

        private void Awake()
        {
            InitializeComponent();
        }

        private void OnEnable()
        {
            if (m_playOnEnable)
            {
                Animate();
            }
        }

        private void OnDisable()
        {
            if (_animatingRoutine != null)
            {
                Stop();
            }
        }

        public override void Animate()
        {
            if (_animatingRoutine != null)
            {
                StopCoroutine(_animatingRoutine);
            }
            _currentProcess = 0f;
            _animationDirection = Direction.Forward;
            _animatingRoutine = StartCoroutine(AnimatingCoroutine(_animationDirection));
            OnStart?.Invoke();
        }

        public override void AnimateReversed()
        {
            if (_animatingRoutine != null)
            {
                StopCoroutine(_animatingRoutine);
            }
            _currentProcess = 0f;
            _animationDirection = Direction.Backward;
            _animatingRoutine = StartCoroutine(AnimatingCoroutine(_animationDirection));
            OnStart?.Invoke();
        }

        public override void Resume()
        {
            if (_animatingRoutine == null) return;
            StopCoroutine(_animatingRoutine);
            _animatingRoutine = StartCoroutine(AnimatingCoroutine(_animationDirection));
            OnResume?.Invoke();
        }

        public override void Pause()
        {
            if (_animatingRoutine == null) return;
            StopCoroutine(_animatingRoutine);
            IsPlaying = false;
            OnPause?.Invoke();
        }

        public override void Stop()
        {
            if (_animatingRoutine == null) return;
            StopCoroutine(_animatingRoutine);
            IsPlaying = false;
            _currentProcess = 0f;
            OnStop?.Invoke();
        }

        public void SetBeginValues()
        {
            SetValue(out m_beginValue);
        }

        public void SetEndValues()
        {
            SetValue(out m_endValue);
        }

        protected void InitializeComponent()
        {
            _targetTransform = this.transform;
        }

        protected IEnumerator AnimatingCoroutine(Direction direction)
        {
            OnAnimationBegin?.Invoke();
            IsPlaying = true;
            
            var waitFor = new WaitForEndOfFrame();
            while (_currentProcess <= m_animationTime)
            {
                UpdateTransform(direction, _currentProcess / m_animationTime);
                _currentProcess += Time.deltaTime;
                yield return waitFor;
            }
            IsPlaying = false;
            OnAnimationEnd?.Invoke();
            ProcessAnimationEnd(direction);
        }

        protected abstract void UpdateTransform(Direction direction, float t);
        protected abstract void SetValue(out Vector3 value);

        private void ProcessAnimationEnd(Direction direction)
        {
            switch (m_animationType)
            {
                case ProceduralAnimation.Single: 
                    return;
                case ProceduralAnimation.Cyclic: 
                    Animate();
                    return;
                case ProceduralAnimation.PingPong:
                    if (direction == Direction.Forward) AnimateReversed();
                    else if (direction == Direction.Backward) Animate();
                    return;
            }
        }
    }
}
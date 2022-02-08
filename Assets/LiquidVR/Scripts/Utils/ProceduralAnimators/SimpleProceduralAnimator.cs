using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Liquid.Core;

namespace Liquid.Utils
{
    public abstract class SimpleProceduralAnimator : BaseProceduralAnimator
    {
        [SerializeField] private IterationType m_animationType = IterationType.Single;
        [SerializeField] private float m_animationTime = 1f;
        [SerializeField] private bool m_playOnEnable = false;
        
        [SerializeField, HideInInspector] private UnityEvent m_onStart = new UnityEvent();
        [SerializeField, HideInInspector] private UnityEvent m_onPause = new UnityEvent();
        [SerializeField, HideInInspector] private UnityEvent m_onResume = new UnityEvent();
        [SerializeField, HideInInspector] private UnityEvent m_onStop = new UnityEvent();
        [SerializeField, HideInInspector] private UnityEvent m_onAnimationBegin = new UnityEvent();
        [SerializeField, HideInInspector] private UnityEvent m_onAnimationEnd = new UnityEvent();

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

        protected IEnumerator AnimatingCoroutine(Direction direction)
        {
            OnAnimationBegin?.Invoke();
            IsPlaying = true;
            
            var waitFor = new WaitForEndOfFrame();
            while (_currentProcess <= m_animationTime)
            {
                UpdateAnimation(direction, _currentProcess / m_animationTime);
                _currentProcess += Time.deltaTime;
                yield return waitFor;
            }
            UpdateAnimation(direction, 1f);

            IsPlaying = false;
            OnAnimationEnd?.Invoke();
            ProcessAnimationEnd(direction);
        }

        private void ProcessAnimationEnd(Direction direction)
        {
            switch (m_animationType)
            {
                case IterationType.Single: 
                    return;
                case IterationType.Cyclic: 
                    Animate();
                    return;
                case IterationType.PingPong:
                    switch (direction)
                    {
                        case Direction.Forward:
                            AnimateReversed();
                            break;
                        case Direction.Backward:
                            Animate();
                            break;
                    }
                    return;
            }
        }

        
        public abstract void SetBeginValues();
        public abstract void SetEndValues();
        protected abstract void InitializeComponent();
        protected abstract void UpdateAnimation(Direction direction, float t);
    }
}
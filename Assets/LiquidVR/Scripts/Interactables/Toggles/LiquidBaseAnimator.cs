using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Liquid.Interactables
{
    public class LiquidBaseAnimator : MonoBehaviour
    {
        [SerializeField] protected float m_animationTime = 1f;
        [SerializeField] private Vector3 m_startPoint = Vector3.zero;
        [SerializeField] private Vector3 m_endPoint = Vector3.zero;
        [SerializeField] private UnityEvent m_onAnimationBegin;
        [SerializeField] private UnityEvent m_onAnimationEnd;

        public Vector3 StartPoint
        {
            get => m_startPoint;
            set => m_startPoint = value;
        }

        public Vector3 EndPoint
        {
            get => m_endPoint;
            set => m_endPoint = value;
        }

        public UnityEvent OnAnimationBegin => m_onAnimationBegin;
        public UnityEvent OnAnimationEnd => m_onAnimationEnd;

        protected LiquidBaseToggle _targetLiquidToggle = null;
        protected bool _initialized = false;
        private Coroutine _animatingRoutine = null;

        protected virtual void Awake()
        {
            InitializeComponent();
        }

        protected virtual void OnEnable() 
        {
            _targetLiquidToggle.OnPress?.AddListener(ProcessPress);
            _targetLiquidToggle.OnRelease?.AddListener(ProcessRelease);
        }

        protected virtual void OnDisable() 
        {
            _targetLiquidToggle.OnPress?.RemoveListener(ProcessPress);
            _targetLiquidToggle.OnRelease?.RemoveListener(ProcessRelease);
        }

        protected virtual void ProcessPress()
        {
            if (_targetLiquidToggle.InteractionType == LiquidHandInteraction.Hold)
            {
                PlayPressAnimation();
            }
            else 
            {
                PlayTouchAnimation();
            }
        }

        protected virtual void ProcessRelease()
        {
            if (_targetLiquidToggle.InteractionType == LiquidHandInteraction.Hold)
            {
                PlayReleaseAnimation();
            }
        }

        public virtual void SetStartPoint()
        {
            m_startPoint = this.transform.localPosition;
        }

        public virtual void SetEndPoint()
        {
            m_endPoint = this.transform.localPosition;
        }

        protected virtual void InitializeComponent()
        {
            if (_initialized) return;
            _targetLiquidToggle = GetComponent<LiquidBaseToggle>();
        }

        protected void PlayPressAnimation()
        {
            PlayAnimation(AnimatePress());
        }

        protected void PlayReleaseAnimation()
        {
            PlayAnimation(AnimateRelease());
        }

        protected void PlayTouchAnimation()
        {
            PlayAnimation(AnimateTouch());
        }

        protected virtual IEnumerator AnimatePress()
        {
            OnAnimationBegin?.Invoke();
            yield return AnimateTarget(this.transform.localPosition, EndPoint);
        }

        protected virtual IEnumerator AnimateRelease()
        {
            yield return AnimateTarget(this.transform.localPosition, StartPoint);
            OnAnimationEnd?.Invoke();
        }

        protected virtual IEnumerator AnimateTarget(Vector3 start, Vector3 end)
        {
            var waitFor = new WaitForEndOfFrame();
            for (var i = 0f; i <= m_animationTime; i += Time.deltaTime)
            {
                this.transform.localPosition = Vector3.Lerp(start, end, i / m_animationTime);
                yield return waitFor;
            }
            this.transform.localPosition = end;
        }

        protected virtual IEnumerator AnimateTouch()
        {
            _targetLiquidToggle.DisableInteractions();
            yield return AnimatePress();
            yield return AnimateRelease();
            _targetLiquidToggle.EnableInteractions();
        }

        private void PlayAnimation(IEnumerator animationCoroutine)
        {
            if (_animatingRoutine != null) 
            {
                StopCoroutine(_animatingRoutine);
            }
            _animatingRoutine = StartCoroutine(animationCoroutine);
        }
    }
}
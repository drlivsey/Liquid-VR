using UnityEngine;
using UnityEngine.Events;
using System;
using System.Linq;
using System.Collections;

namespace Liquid.Utils
{
    public class AnimatorTrigger : MonoBehaviour
    {
        [SerializeField] private Animator m_targetAnimator = null;
        [SerializeField] private AnimatorState[] m_states = null;

        public bool IsPlaying => _isPlaying;        
        private bool _isActive = true;
        private bool _isPlaying = false;

        public void SetActive(bool state) => _isActive = state;

        public void Play(int index)
        {
            if (_isActive == false) return;
            if (index >= m_states.Length) return;
            
            var state = m_states[index];
            StartCoroutine(PlayAnimation(state));
        }

        public void Play(string name)
        {
            if (_isActive == false) return;
            if (m_states.Any(x => x.Name.Equals(name)))
            {
                var state = m_states.First(x => x.Name.Equals(name));
                StartCoroutine(PlayAnimation(state));
            }
        }

        public bool TryPlay(int index)
        {
            if (_isActive == false) return false;
            if (index >= m_states.Length) return false;

            var state = m_states[index];
            StartCoroutine(PlayAnimation(state));
            return true;
        }

        public bool TryPlay(string name)
        {
            if (_isActive == false) return false;
            if (m_states.Any(x => x.Name.Equals(name)) == false)
                return false;

            var state = m_states.First(x => x.Name.Equals(name));
            StartCoroutine(PlayAnimation(state));
            return true;
        }

        public void Pause()
        {
            SetSpeed(0f);
        }

        public void Resume()
        {
            SetSpeed(1f);
        }

        public void SetSpeed(float speed)
        {
            m_targetAnimator.speed = speed;
        }

        public void SetBool(string name)
        {
            m_targetAnimator.SetBool(name, true);
        }

        public void ResetBool(string name)
        {
            m_targetAnimator.SetBool(name, false);
        }

        public void SetTrigger(string name)
        {
            m_targetAnimator.SetTrigger(name);
        }

        public void ResetTrigger(string name)
        {
            m_targetAnimator.ResetTrigger(name);
        }

        private IEnumerator PlayAnimation(AnimatorState state)
        {
            state.OnAnimationStart?.Invoke();
            
            m_targetAnimator.speed = 1f;
            m_targetAnimator.Play(state.Name, state.Layer);
            
            yield return new WaitForEndOfFrame();
            
            _isPlaying = true;
            
            yield return new WaitForSeconds(m_targetAnimator.GetCurrentAnimatorStateInfo(state.Layer).length);

            _isPlaying = false;
            
            state.OnAnimationEnd?.Invoke();
        }
    }

    [Serializable] public class AnimatorState
    {
        [SerializeField] private string _stateName = string.Empty;
        [SerializeField] private int _animatorLayer = -1;
        [SerializeField, Space(10)] private UnityEvent m_onAnimationStart;
        [SerializeField] private UnityEvent m_onAnimationEnd;

        public string Name => _stateName;
        public int Layer => _animatorLayer;
        public UnityEvent OnAnimationStart => m_onAnimationStart;
        public UnityEvent OnAnimationEnd => m_onAnimationEnd;
    }
}
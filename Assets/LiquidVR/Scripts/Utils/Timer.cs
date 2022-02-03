using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Liquid.Utils
{
    public class Timer : MonoBehaviour
    {
        [SerializeField] private bool m_startOnAwake = false;
        [SerializeField] private UnityEvent m_onTick = new UnityEvent();
 
        public int Seconds 
        {
            get; private set;
        }
        public int Minutes
        {
            get; private set;
        }
        public int Hours
        {
            get; private set;
        }
        public int Time => (Hours * 3600) + (Minutes * 60) + Seconds;
        public UnityEvent OnTick => m_onTick;

        private Coroutine _timerCoroutine = null;

        private void Awake()
        {
            if (m_startOnAwake)
            {
                Play();
            }
        }   

        public void Play()
        {
            if (_timerCoroutine != null)
            {
                StopCoroutine(_timerCoroutine);
            }

            _timerCoroutine = StartCoroutine(Tick());
        }

        public void Pause()
        {
            if (_timerCoroutine != null)
            {
                StopCoroutine(_timerCoroutine);
            }
        }

        public void Stop()
        {
            Pause();
            Seconds = Minutes = Hours = 0;
        }

        private IEnumerator Tick()
        {
            var waitForSecond = new WaitForSecondsRealtime(1f);
            while (this.gameObject.activeInHierarchy)
            {
                yield return waitForSecond;

                Seconds += 1;

                if (Seconds >= 60)
                {
                    Seconds = 0;
                    Minutes += 1;
                }

                if (Minutes >= 60)
                {
                    Minutes = 0;
                    Hours += 1;
                }

                OnTick?.Invoke();
            }
        }    
    }
}
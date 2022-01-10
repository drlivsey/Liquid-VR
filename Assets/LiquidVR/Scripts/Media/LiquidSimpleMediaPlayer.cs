using UnityEngine;
using UnityEngine.Events;

namespace Liquid.Media
{
    public abstract class LiquidSimpleMediaPlayer : LiquidBaseMediaPlayer
    {
        [SerializeField] private UnityEvent m_onPlay = new UnityEvent();
        [SerializeField] private UnityEvent m_onPause = new UnityEvent();
        [SerializeField] private UnityEvent m_onStop = new UnityEvent();
        [SerializeField] private UnityEvent m_onReset = new UnityEvent();
        
        public UnityEvent OnPlay => m_onPlay;
        public UnityEvent OnPause => m_onPause;
        public UnityEvent OnStop => m_onStop;
        public UnityEvent OnReset => m_onReset;

        private void Awake()
        {
            InitializeComponent();
        }

        protected abstract void InitializeComponent();
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Video;

namespace Liquid.Media
{
    [RequireComponent(typeof(VideoPlayer), typeof(AudioSource))]
    public class LiquidVideoPlayer : MonoBehaviour
    {
        [SerializeField] private UnityEvent m_onPlay = new UnityEvent();
        [SerializeField] private UnityEvent m_onPause = new UnityEvent();
        [SerializeField] private UnityEvent m_onStop = new UnityEvent();
        [SerializeField] private UnityEvent m_onReset = new UnityEvent();
        [SerializeField] private UnityEvent m_onPrepare = new UnityEvent();

        private VideoPlayer _targetVideoPlayer = null;
        private AudioSource _targetAudioSource = null;

        public float PlaybackProgress
        {
            get => (float)(_targetVideoPlayer.time / _targetVideoPlayer.length);
        }
        public float Time
        {
            get => (float)_targetVideoPlayer.time;
        }
        public float Length
        {
            get => (float)_targetVideoPlayer.length;
        }
        public bool IsPrepared
        {
            get => _targetVideoPlayer.isPrepared;
        }
        public bool IsPaused
        {
            get => _targetVideoPlayer.isPaused;
        }
        public bool IsPlaying 
        {
            get => _targetVideoPlayer.isPlaying;
        }
        public float SoundVolume
        {
            get => _targetAudioSource.volume;
        }

        public UnityEvent OnPlay => m_onPlay;
        public UnityEvent OnPause => m_onPause;
        public UnityEvent OnStop => m_onStop;
        public UnityEvent OnReset => m_onReset;
        public UnityEvent OnPrepare => m_onPrepare;

        private void Awake()
        {
            InitializeComponent();
        }

        public void SetVideoClip(VideoClip clip)
        {
            _targetVideoPlayer.Stop();
            _targetVideoPlayer.clip = clip;
        }

        public void SetTime(float time)
        {
            if (_targetVideoPlayer.isPrepared)
            {
                _targetVideoPlayer.time = time;
            }
        }

        public void SetTimeClamped(float time)
        {
            if (_targetVideoPlayer.isPrepared)
            {
                var clampedTime = time / _targetVideoPlayer.length;
            }
        }

        public void SetPlaybackProgress(float progress)
        {
            if (_targetVideoPlayer.isPrepared)
            {
                _targetVideoPlayer.time = _targetVideoPlayer.length * progress;
            }
        }

        public void SetSoundVolume(float value)
        {
            _targetAudioSource.volume = value;
        }

        public void Play()
        {
            _targetVideoPlayer.Prepare();
            _targetVideoPlayer.Play();
        }

        public void Pause()
        {
            if (_targetVideoPlayer.isPlaying)
            {
                _targetVideoPlayer.Pause();
            }
        }

        public void Stop()
        {
            if (_targetVideoPlayer.isPlaying)
            {
                _targetVideoPlayer.Stop();
            }
        }

        public void ResetVideo()
        {
            if (_targetVideoPlayer.isPrepared)
            {
                _targetVideoPlayer.time = 0f;
            }
        }

        public IEnumerator Prepare()
        {
            _targetVideoPlayer.Prepare();
            do 
            {
                yield return new WaitForEndOfFrame();
            } 
            while (_targetVideoPlayer.isPrepared == false);
            OnPrepare?.Invoke();
        }

        public IEnumerator SetVideoClipAndPrepare(VideoClip clip)
        {
            SetVideoClip(clip);
            yield return Prepare();
        }

        private void InitializeComponent()
        {
            _targetVideoPlayer = GetComponent<VideoPlayer>();
            _targetAudioSource = GetComponent<AudioSource>();

            _targetVideoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
            _targetVideoPlayer.SetTargetAudioSource(0, _targetAudioSource);
            StartCoroutine(Prepare());
        }
    }
}
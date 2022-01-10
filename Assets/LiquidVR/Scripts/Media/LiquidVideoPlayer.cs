using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Video;

namespace Liquid.Media
{
    [RequireComponent(typeof(VideoPlayer), typeof(AudioSource))]
    public class LiquidVideoPlayer : LiquidSimpleMediaPlayer
    {
        [SerializeField] private UnityEvent m_onPrepare = new UnityEvent();

        private VideoPlayer _targetVideoPlayer = null;
        private AudioSource _targetAudioSource = null;

        public override float Time => (float)_targetVideoPlayer.time;
        public override float Length => (float)_targetVideoPlayer.length;
        
        public override bool IsPaused => _targetVideoPlayer.isPaused;
        public override bool IsPlaying => _targetVideoPlayer.isPlaying;
        public override float SoundVolume
        {
            get => _targetAudioSource.volume;
            set => _targetAudioSource.volume = value;
        }
        public bool IsPrepared => _targetVideoPlayer.isPrepared;
        
        public UnityEvent OnPrepare => m_onPrepare;

        public override void SetSource(object source)
        {
            if (source.GetType() != typeof(VideoClip))
            {
                Debug.LogError($"{source.GetType()} is not a VideoClip!", this.gameObject);
                return;
            }
            _targetVideoPlayer.Stop();
            _targetVideoPlayer.clip = source as VideoClip;
        }

        public override void SetTime(float time)
        {
            if (_targetVideoPlayer.isPrepared)
            {
                _targetVideoPlayer.time = time;
            }
        }

        public override void Play()
        {
            _targetVideoPlayer.Prepare();
            _targetVideoPlayer.Play();
            OnPlay?.Invoke();
        }

        public override void Pause()
        {
            if (_targetVideoPlayer.isPlaying)
            {
                _targetVideoPlayer.Pause();
                OnPause?.Invoke();
            }
        }

        public override void Stop()
        {
            if (_targetVideoPlayer.isPlaying)
            {
                _targetVideoPlayer.Stop();
                OnStop?.Invoke();
            }
        }

        public override void ResetMedia()
        {
            if (_targetVideoPlayer.isPrepared)
            {
                _targetVideoPlayer.time = 0f;
                OnReset?.Invoke();
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
            SetSource(clip);
            yield return Prepare();
        }

        protected override void InitializeComponent()
        {
            _targetVideoPlayer = GetComponent<VideoPlayer>();
            _targetAudioSource = GetComponent<AudioSource>();

            _targetVideoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
            _targetVideoPlayer.SetTargetAudioSource(0, _targetAudioSource);
            StartCoroutine(Prepare());
        }
    }
}
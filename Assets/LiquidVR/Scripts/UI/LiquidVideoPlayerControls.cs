using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Liquid.Media;
using Liquid.Core;

namespace Liquid.UI
{
    [RequireComponent(typeof(LiquidVideoPlayer))]
    public class LiquidVideoPlayerControls : MonoBehaviour
    {
        [SerializeField] private Button m_playPauseButton = null;
        [SerializeField] private Button m_stopButton = null;
        [SerializeField] private Button m_resetButton = null;
        [SerializeField] private Button m_rewindForwardButton = null;
        [SerializeField] private Button m_rewindBackButton = null;
        [SerializeField] private LiquidPlaybackSlider m_playbackProgressBar = null;
        [SerializeField] private Slider m_soundVolumeSlider = null;
        [SerializeField] private TMP_Text m_playbackTimeField = null;

        private LiquidVideoPlayer _targetVideoPlayer = null;
        
        private void Awake()
        {
            InitializeComponent();
        }

        private void Update()
        {
            UpdatePlaybackTime();
            UpdatePlaybackPogress();
        }

        private void OnEnable() 
        {
            AddListeners();
        }

        private void OnDisable() 
        {
            RemoveListeners();
        }

        private void InitializeComponent()
        {
            _targetVideoPlayer = GetComponent<LiquidVideoPlayer>();
        }

        private void AddListeners()
        {
            _targetVideoPlayer.OnPrepare?.AddListener(SetupPlayback);
            
            if (m_playPauseButton)
            {
                m_playPauseButton.onClick.AddListener(SwitchPlaybackState);
            }
            if (m_rewindForwardButton)
            {
                m_rewindForwardButton.onClick.AddListener(RewindForward);
            }
            if (m_rewindBackButton)
            {
                m_rewindBackButton.onClick.AddListener(RewindBack);
            }
            if (m_resetButton)
            {
                m_resetButton.onClick.AddListener(ResetVideo);
            }
            if (m_stopButton)
            {
                m_stopButton.onClick.AddListener(Stop);
            }
            if (m_playbackProgressBar)
            {
                m_playbackProgressBar.onValueChanged.AddListener(SetPlaybackProgress);
                m_playbackProgressBar.OnHandlePointerDown.AddListener(Pause);
                m_playbackProgressBar.OnHandlePointerUp.AddListener(Play);
            }
            if (m_soundVolumeSlider)
            {
                m_soundVolumeSlider.onValueChanged.AddListener(SetSoundVolume);
            }
        }

        private void RemoveListeners()
        {
            _targetVideoPlayer.OnPrepare?.RemoveListener(SetupPlayback);
            
            if (m_playPauseButton)
            {
                m_playPauseButton.onClick.RemoveListener(SwitchPlaybackState);
            }
            if (m_rewindForwardButton)
            {
                m_rewindForwardButton.onClick.RemoveListener(RewindForward);
            }
            if (m_rewindBackButton)
            {
                m_rewindBackButton.onClick.RemoveListener(RewindBack);
            }
            if (m_resetButton)
            {
                m_resetButton.onClick.RemoveListener(ResetVideo);
            }
            if (m_stopButton)
            {
                m_stopButton.onClick.RemoveListener(Stop);
            }
            if (m_playbackProgressBar)
            {
                m_playbackProgressBar.onValueChanged.RemoveListener(SetPlaybackProgress);
                m_playbackProgressBar.OnHandlePointerDown.RemoveListener(Pause);
                m_playbackProgressBar.OnHandlePointerUp.RemoveListener(Play);
            }
            if (m_soundVolumeSlider)
            {
                m_soundVolumeSlider.onValueChanged.RemoveListener(SetSoundVolume);
            }
        }

        private void SetupPlayback()
        {
            if (m_playbackProgressBar)
            {
                m_playbackProgressBar.minValue = 0f;
                m_playbackProgressBar.maxValue = _targetVideoPlayer.Length;
            }
            if (m_soundVolumeSlider)
            {
                m_soundVolumeSlider.minValue = 0f;
                m_soundVolumeSlider.maxValue = 1f;
                m_soundVolumeSlider.value = _targetVideoPlayer.SoundVolume;
            }
        }

        private void SwitchPlaybackState()
        {
            if (_targetVideoPlayer.IsPlaying == false)
            {
                Play();
            }
            else 
            {
                Pause();
            }
        }

        private void Play()
        {
            _targetVideoPlayer.Play();
        }

        private void Pause()
        {
            _targetVideoPlayer.Pause();
        }

        private void Stop()
        {
            _targetVideoPlayer.Stop();
        }

        private void ResetVideo()
        {
            _targetVideoPlayer.ResetMedia();
            UpdateProgressBarValue();
            UpdateTimeValue();
        }

        private void RewindForward()
        {
            var rewindedTime = _targetVideoPlayer.Time + 10f;
            _targetVideoPlayer.SetTime(rewindedTime > _targetVideoPlayer.Length ? _targetVideoPlayer.Length : rewindedTime);
        }

        private void RewindBack()
        {
            var rewindedTime = _targetVideoPlayer.Time - 10f;
            _targetVideoPlayer.SetTime(rewindedTime < 0f ? 0f : rewindedTime);
        }

        private void UpdatePlaybackTime()
        {
            if (m_playbackTimeField == null) return;
            if (_targetVideoPlayer.IsPlaying == false) return;

            UpdateTimeValue();
        }

        private void UpdatePlaybackPogress()
        {
            if (m_playbackProgressBar == null) return;
            if (_targetVideoPlayer.IsPlaying == false) return;

            UpdateProgressBarValue();
        }

        private void SetPlaybackProgress(float time)
        {
            _targetVideoPlayer.SetTime(time);
        }

        private void SetSoundVolume(float volume)
        {
            _targetVideoPlayer.SoundVolume = volume;
        }

        private void UpdateProgressBarValue()
        {
            var value = _targetVideoPlayer.Time;
            m_playbackProgressBar.value = value;
        }

        private void UpdateTimeValue()
        {
            var time = _targetVideoPlayer.Time;
            var formatedTime = Converter.SecondsToTime(time);
            m_playbackTimeField.text = formatedTime;
        }
    }
}
using System.Collections;
using UnityEngine;
using UnityEngine.Video;

namespace Liquid.Media
{
    [RequireComponent(typeof(LiquidVideoPlayer))]
    public class LiquidVideoPlayerPlaylist : MonoBehaviour
    {
        [SerializeField] private VideoClip[] m_videoClips = null;
        [SerializeField] private bool m_loop = false;
        [SerializeField] private bool m_autoplay = false;

        private LiquidVideoPlayer _targetVideoPlayer = null;
        private int _currentVideoClipIndex = 0;

        private void Awake()
        {
            InitializeComponent();
        }

        private void FixedUpdate()
        {
            if (m_autoplay == false) return;
            if (_targetVideoPlayer.Time >= _targetVideoPlayer.Length - 0.05f)
            {
                PlayNext();
            }
        }

        public void PlayNext()
        {
            if (m_videoClips == null) return;

            var index = _currentVideoClipIndex + 1;
            if (index >= m_videoClips.Length)
            {
                if (m_loop) index = 0;
                else return;
            }
            PlayByIndex(index);
        }

        public void PlayPrevious()
        {
            if (m_videoClips == null) return;

            var index = _currentVideoClipIndex - 1;
            if (index < 0)
            {
                if (m_loop) index = m_videoClips.Length - 1;
                else return;
            }
            PlayByIndex(index);
        }

        public void PlayByIndex(int index)
        {
            if (index < 0 || index >= m_videoClips.Length) return;

            _currentVideoClipIndex = index;
            StartCoroutine(SwitchVideo(m_videoClips[_currentVideoClipIndex]));
        }

        private void InitializeComponent()
        {
            _targetVideoPlayer = GetComponent<LiquidVideoPlayer>();
        }

        private IEnumerator SwitchVideo(VideoClip clip)
        {
            yield return _targetVideoPlayer.SetVideoClipAndPrepare(clip);
            _targetVideoPlayer.Play();
        }
    }
}
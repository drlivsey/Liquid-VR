using UnityEngine;

namespace Liquid.Translation
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioTranslator : BaseTranslator
    {
        [SerializeField] private AudioSource m_translationTarget = null;

        public override void Translate(string cultureValue)
        {
            if (TryLoadAudio(cultureValue, out var requiredClip))
                m_translationTarget.clip = requiredClip;
        }

        public override void InitializeComponent()
        {
            m_translationTarget = m_translationTarget == null ? GetComponent<AudioSource>() : m_translationTarget;
        }

        private bool TryLoadAudio(string clipName, out AudioClip clip)
        {
            clip = null;

            var loadedClip = Resources.Load<AudioClip>(string.Join("/", TranslatorConstants.AudioResourcesFolderPath, clipName));

            if (loadedClip == null)
                return false;

            clip = loadedClip;
            return true;
        }
    }
}
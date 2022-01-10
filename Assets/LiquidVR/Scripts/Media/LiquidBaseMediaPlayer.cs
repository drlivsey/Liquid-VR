using UnityEngine;

namespace Liquid.Media
{
    public abstract class LiquidBaseMediaPlayer : MonoBehaviour
    {
        public abstract bool IsPaused { get; }
        public abstract bool IsPlaying { get; }
        public abstract float Time { get; }
        public abstract float Length { get; }
        public abstract float SoundVolume { get; set; }
        
        public abstract void Play();
        public abstract void Pause();
        public abstract void Stop();
        public abstract void ResetMedia();
        public abstract void SetSource(object source);
        public abstract void SetTime(float time);
    }
}
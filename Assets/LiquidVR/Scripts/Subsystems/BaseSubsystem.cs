using UnityEngine;

namespace Liquid.Subsystems
{
    public abstract class BaseSubsystem : MonoBehaviour
    {
        public abstract void Initialize();
        public abstract void StartExecution();
        public abstract void StopExecution();
        public abstract void Dispose();
    }
}
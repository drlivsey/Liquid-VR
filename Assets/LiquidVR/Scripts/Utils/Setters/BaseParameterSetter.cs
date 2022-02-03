using UnityEngine;

namespace Liquid.Utils.Setters
{
    public abstract class BaseParameterSetter : MonoBehaviour
    {
        public abstract void SetParameter();
        public abstract void RestoreParameter();
    }
}

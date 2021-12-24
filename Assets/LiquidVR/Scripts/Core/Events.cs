using System;
using UnityEngine;
using UnityEngine.Events;

namespace Liquid.Core
{
    [Serializable] public class FloatEvent : UnityEvent<float> { }
    [Serializable] public class Int32Event : UnityEvent<int> { }
    [Serializable] public class BoolEvent : UnityEvent<bool> { }
    [Serializable] public class Vector3Event : UnityEvent<Vector3> { }
    [Serializable] public class QuaternionEvent : UnityEvent<Quaternion> { }
    [Serializable] public class ColorEvent : UnityEvent<Color> { }
}
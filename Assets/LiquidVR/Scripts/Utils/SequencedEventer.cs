using UnityEngine;
using UnityEngine.Events;
using Liquid.Core;

namespace Liquid.Utils
{
    public class SequencedEventer : MonoBehaviour
    {
        [SerializeField] private IterationType m_iterationType = IterationType.Single;
        [SerializeField] private UnityEvent[] m_eventsSequence = null;

        public IterationType IterationType
        {
            get => m_iterationType;
            set => m_iterationType = value;
        }

        public UnityEvent[] EventsList
        {
            get => m_eventsSequence;
            set => m_eventsSequence = value;
        }

        private int _currentIndex = -1;
        private Direction _iterationDirection = Direction.Forward;

        public void InvokeNext()
        {
            if (TryGetNextEventIndex(out var index, m_iterationType))
            {
                InvokeByIndex(index);
            }
        }

        public void InvokePrevious()
        {
            if (TryGetPreviousEventIndex(out var index, m_iterationType))
            {
                InvokeByIndex(index);
            }
        }

        public void InvokeByIndex(int index)
        {
            if (index >= EventsList.Length || index < 0) return;
            _currentIndex = index;
            EventsList[index]?.Invoke();
        }

        private bool TryGetNextEventIndex(out int index, IterationType mode)
        {
            switch (mode)
            {
                case IterationType.Cyclic:
                {
                    return TryGetNextIndexForCyclicMode(_currentIndex, out index);
                }
                case IterationType.Single:
                {
                    return TryGetNextIndexForSingleMode(_currentIndex, out index);
                }
                case IterationType.PingPong:
                {
                    return TryGetNextIndexForPingPongMode(_currentIndex, out index);
                }
                default:
                {
                    index = _currentIndex;
                    return false;
                }
            }
        }

        private bool TryGetPreviousEventIndex(out int index, IterationType mode)
        {
            switch (mode)
            {
                case IterationType.Cyclic:
                {
                    return TryGetPreviousIndexForCyclicMode(_currentIndex, out index);
                }
                case IterationType.Single:
                {
                    return TryGetPreviousIndexForSingleMode(_currentIndex, out index);
                }
                case IterationType.PingPong:
                {
                    return TryGetPreviousIndexForPingPongMode(_currentIndex, out index);
                }
                default:
                {
                    index = _currentIndex;
                    return false;
                }
            }
        }

        private void SwitchIterationDirection()
        {
            _iterationDirection = _iterationDirection == Direction.Forward ? Direction.Backward : Direction.Forward;
        }

        private bool TryGetNextIndexForCyclicMode(int currentIndex, out int index)
        {
            if (currentIndex + 1 >= EventsList.Length)
            {
                index = 0;
            }
            else
            {
                index = (currentIndex + 1);
            }
                    
            return true;
        }

        private bool TryGetNextIndexForSingleMode(int currentIndex, out int index)
        {
            if (currentIndex + 1 >= EventsList.Length)
            {
                index = currentIndex;
                return false;
            }
                    
            index = (currentIndex + 1);
            return true;
        }
        
        private bool TryGetNextIndexForPingPongMode(int currentIndex, out int index)
        {
            if (currentIndex + 1 >= EventsList.Length || currentIndex - 1 < 0)
            {
                SwitchIterationDirection();
            }

            index = _iterationDirection == Direction.Forward ? (currentIndex + 1) : (currentIndex - 1);
            return true;
        }
        
        private bool TryGetPreviousIndexForCyclicMode(int currentIndex, out int index)
        {
            if (currentIndex - 1 < 0)
            {
                index = (EventsList.Length - 1);
            }
            else
            {
                index = (currentIndex - 1);
            }
                    
            return true;
        }

        private bool TryGetPreviousIndexForSingleMode(int currentIndex, out int index)
        {
            if (currentIndex - 1 < 0)
            {
                index = currentIndex;
                return false;
            }
                    
            index = (currentIndex - 1);
            return true;
        }
        
        private bool TryGetPreviousIndexForPingPongMode(int currentIndex, out int index)
        {
            if (currentIndex + 1 >= EventsList.Length || currentIndex - 1 < 0)
            {
                SwitchIterationDirection();
            }

            index = _iterationDirection == Direction.Forward ? (currentIndex - 1) : (currentIndex + 1);
            return true;
        }
    }
}
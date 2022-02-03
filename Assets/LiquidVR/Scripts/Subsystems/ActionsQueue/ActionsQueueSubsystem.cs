using System;
using System.Collections.Generic;
using UnityEngine;

namespace Liquid.Subsystems.ActionsQueue
{
    public class ActionsQueueSubsystem : BaseSubsystem
    {
        private Queue<Action> _actions = null;
        private static ActionsQueueSubsystem _instance = null;
        
        public bool IsPerformed
        {
            get; private set;
        }

        public bool IsInitialized
        {
            get; private set;
        }

        private void Reset()
        {
            SetupInstance();
        }

        private void OnEnable()
        {
            StartExecution();
        }

        private void OnDisable()
        {
            StopExecution();
        }

        private void OnDestroy()
        {
            Dispose();
        }

        private void Update()
        {
            if (IsInitialized == false) return;
            if (IsPerformed == false) return;
            
            lock (_actions)
            {
                if (_actions.Count == 0) return;
                
                var action = _actions.Dequeue();
                action?.Invoke();
            }
        }

        public override void Initialize()
        {
            if (IsInitialized) return;
            
            _actions = new Queue<Action>();
            IsInitialized = true;
        }

        public override void StartExecution()
        {
            IsPerformed = true;
        }

        public override void StopExecution()
        {
            IsPerformed = false;
        }

        public override void Dispose()
        {
            StopExecution();
            lock (_actions)
            {
                _actions.Clear();
                _actions = null;
            }

            IsInitialized = false;
            IsPerformed = false;
        }

        public void RegisterAction(Action action)
        {
            if (IsInitialized == false) return;

            lock (_actions)
            {
                _actions.Enqueue(action);
            }
        }

        public static ActionsQueueSubsystem SubsystemInstance()
        {
            if (_instance != null) return _instance;
            
            var subsystemGameObject = new GameObject("[ActionsQueueSubsystem]");
            _instance = subsystemGameObject.AddComponent<ActionsQueueSubsystem>();
            _instance.Initialize();
            return _instance;
        }

        private void SetupInstance()
        {
            if (_instance == this) return;
            if (_instance == null)
            {
                _instance = this;
                _instance.Initialize();
                this.gameObject.name = "[ActionsQueueSubsystem]";
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }
}

